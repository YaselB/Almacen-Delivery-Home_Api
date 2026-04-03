using AlmacenApi.Domain.ComboOutDtoClass;
using AlmacenApi.Domain.Common;
using AlmacenApi.Domain.Entities.Admin;
using AlmacenApi.Domain.Entities.Combo;
using AlmacenApi.Domain.Entities.User;
using AlmacenApi.Domain.Events.ComboOut;

namespace AlmacenApi.Domain.Entities.CombOut;

public class ComboOutEntity : GenericEntity<ComboOutEntity>
{
    public string ComboName { get; set; } = string.Empty;
    public string OutMotive { get; set; } = string.Empty;
    public string ComboId { get; set; } = string.Empty;
    public string? AdminId { get; set; }
    public string? UserId { get; set; }
    public string? Customer { get; set; }
    public List<string>? ProductsId { get; set; }
    public List<int>? Quantity { get; set; }
    public ComboEntity? Combo { get; set; }
    public AdminEntity? Admin { get; set; }
    public UserEntity? User { get; set; }
    public static ComboOutEntity Create(string ComboId, string? UserId, string? AdminId, string OutMotive, string ComboName, List<ComboOutDto>? ComboUpdate, string? Customer)
    {
        var comboOut = new ComboOutEntity
        {
            ComboId = ComboId,
            ComboName = ComboName,
            OutMotive = OutMotive,
            Customer = Customer
        };
        if (ComboUpdate != null && ComboUpdate.Count > 0)
        {
            comboOut.ProductsId = new List<string>();
            comboOut.Quantity = new List<int>();

            foreach (var item in ComboUpdate)
            {
                comboOut.ProductsId.Add(item.ProductId);
                comboOut.Quantity.Add(item.Quantity);
            }
        }
        if (UserId != null)
        {
            comboOut.UserId = UserId;
        }
        if (AdminId != null)
        {
            comboOut.AdminId = AdminId;
        }
        var CreateComboOutDomainEvent = new ComboOutCreateEvent(comboOut.ComboName, comboOut.id);
        comboOut.AddDomainEvent(CreateComboOutDomainEvent);
        return comboOut;
    }
}