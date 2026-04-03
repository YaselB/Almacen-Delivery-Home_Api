using AlmacenApi.Aplication.Common.Interfaces.Jwt;
using AlmacenApi.Aplication.Interfaces.Password;
using AlmacenApi.Common.Interfaces.Repository.ProductComboRepository;
using AlmacenApi.Domain.Application.Service;
using AlmacenApi.Domain.Common.Interfaces.Repository.AdminRepo;
using AlmacenApi.Domain.Common.Interfaces.Repository.Combo;
using AlmacenApi.Domain.Common.Interfaces.Repository.ComboOut;
using AlmacenApi.Domain.Common.Interfaces.Repository.Product;
using AlmacenApi.Domain.Common.Interfaces.Repository.ProductOut;
using AlmacenApi.Domain.Common.Interfaces.Repository.UserRepository;
using AlmacenApi.Domain.Repository.Generic;
using AlmacenApi.Domain.Repository.History;
using AlmacenApi.Infrastructure.DBContext;
using AlmacenApi.Infrastructure.Repository.Admin_repository;
using AlmacenApi.Infrastructure.Repository.Combo;
using AlmacenApi.Infrastructure.Repository.ComboOut;
using AlmacenApi.Infrastructure.Repository.Generic;
using AlmacenApi.Infrastructure.Repository.History;
using AlmacenApi.Infrastructure.Repository.Product;
using AlmacenApi.Infrastructure.Repository.ProductCombo;
using AlmacenApi.Infrastructure.Repository.ProductOut;
using AlmacenApi.Infrastructure.Repository.UserRespository;
using AlmacenApi.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlmacenApi.Infrastructure.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services , IConfiguration configuration)
    {
        services.AddDbContext<AppDBContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(AppDBContext).Assembly.FullName))
        );
        services.AddScoped(typeof(IGenericRepository<>) , typeof(GenericRepository<>));
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IUserRepository , UserRepository>();
        services.AddScoped<IProductRepository ,ProductRepository>();
        services.AddScoped<IComboRepository , ComboRepository>();
        services.AddScoped<IProductComboRepository , ProductComboRepository>();
        services.AddScoped<IComboOutRepository , ComboOutRepository>();
        services.AddScoped<IProductOutRepository , ProductOutRepository>();
        services.AddScoped<IHistoryRepository , HistoryRepository>();
        services.AddScoped<IPasswordHashed ,PasswordHashed>();
        services.AddScoped<ComboStockService>();
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddScoped<IJwtGenerator , JwtGenerator>();
        return services;
    }
}