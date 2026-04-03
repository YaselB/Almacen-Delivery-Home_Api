using System.Reflection;
using Microsoft.EntityFrameworkCore;
using AlmacenApi.Domain.Common;
using MediatR;
using AlmacenApi.Domain.Entities.Admin;
using AlmacenApi.Domain.Entities.User;
using AlmacenApi.Domain.Entities.Product;
using AlmacenApi.Domain.Entities.Combo;
using AlmacenApi.Domain.Entities.ProductCombo;
using AlmacenApi.Domain.Entities.Out.ProductOut;
using AlmacenApi.Domain.Entities.CombOut;
using AlmacenApi.Domain.Entities.History;

namespace AlmacenApi.Infrastructure.DBContext;

public class AppDBContext : DbContext
{
    private readonly IMediator mediator;
    public AppDBContext(DbContextOptions<AppDBContext> model, IMediator mediator) : base(model)
    {
        this.mediator = mediator;
    }
    public DbSet<AdminEntity> Admins { get; set; }
    public DbSet<UserEntity> User {get; set ;}
    public DbSet<ProductEntity> Products {get ; set ;}
    public DbSet<ComboEntity> Combo { get ; set ;}
    public DbSet<ProductComboEntity> ProductComboEntity {get ; set ;}
    public DbSet<ProductOutEntity> ProductOut {get ; set ;}
    public DbSet<ComboOutEntity> ComboOut {get ; set ;}
    public DbSet<HistoryEntity> History {get ; set ;}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entityTypes = Assembly.GetAssembly(typeof(GenericEntity<>))?.GetTypes().Where(t => t.IsClass)
                .ToList();
        base.OnModelCreating(modelBuilder);
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;

            // Verificar si la entidad hereda de GenericEntity<>
            if (clrType.BaseType?.IsGenericType == true &&
                clrType.BaseType.GetGenericTypeDefinition() == typeof(GenericEntity<>))
            {
                modelBuilder.Entity(clrType).Ignore("domainEvents");
            }
        }
        modelBuilder.Entity<ProductEntity>(entity =>
        {
            entity.HasOne(p => p.adminEntity).WithMany(p=> p.Products).HasForeignKey(p => p.CreateByAdmin);
            entity.HasOne(p => p.userEntity).WithMany(p => p.Products).HasForeignKey(p => p.CreateByUser);
            entity.HasOne(p => p.Combo).WithMany(p => p.Products).HasForeignKey(p => p.ComboId);
        });
        modelBuilder.Entity<ComboEntity>(entity =>
        {
            entity.HasOne(c => c.Admin).WithMany(c => c.Combos).HasForeignKey(c => c.AdminId);
            entity.HasOne(c => c.User).WithMany(c => c.Combos).HasForeignKey(c => c.UserId);
        });
        modelBuilder.Entity<ProductComboEntity>(entity =>
        {
            entity.HasOne(p => p.comboEntity).WithMany(p => p.ProductComboEntities).HasForeignKey(p => p.ComboId);
            entity.HasOne(p => p.ProductEntity).WithMany(p => p.ProductCombos).HasForeignKey(p => p.ProductId);
        });
        modelBuilder.Entity<ProductOutEntity>(entity =>
        {
            entity.HasOne(p => p.Product).WithMany(p => p.ProductOut).HasForeignKey(p => p.ProductId);
            entity.HasOne(p => p.User).WithMany(p => p.ProductOut).HasForeignKey(p => p.UserId);
            entity.HasOne(p => p.Admin).WithMany(p => p.ProductOut).HasForeignKey(p => p.AdminId);
        });
        modelBuilder.Entity<ComboOutEntity>(entity =>
        {
            entity.HasOne(p => p.Combo).WithMany(p => p.ComboOut).HasForeignKey(p => p.ComboId);
            entity.HasOne(p => p.Admin).WithMany(p => p.ComboOut).HasForeignKey(p =>p.AdminId);
            entity.HasOne(p => p.User).WithMany(p => p.ComboOut).HasForeignKey(p => p.UserId);
        });
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Obtener entidades que implementan IHasDomainEvents
        var domainEventEntities = ChangeTracker.Entries<IHasDomainEvents>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToList();

        var domainEvents = domainEventEntities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent, cancellationToken);
        }

        foreach (var entity in domainEventEntities)
        {
            entity.ClearDomainEvents();
        }

        return result;
    }
}