
namespace AlmacenApi.Aplication.Features.Product.Dto;
public class ProductResultDto 
{
    public required string id {get;set;}
    public required string name{get ;set ;}
    public required double Quantity {get; set ;}
    public required string Unity { get; set ;}
    public required DateTime endDate { get ; set ;}
    public required string? NameUserorAdmin {get ; set ;}
    public required string Category { get ; set ;}
    public required string Provider {get ; set ;}
    public required DateTime UpdateAt {get ; set ;}
    
}