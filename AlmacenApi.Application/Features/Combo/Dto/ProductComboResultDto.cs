namespace AlmacenApi.Aplication.Features.Combo.Dto;
public class ProductcomboResultDto
{
    public required string? id {get ; set ;}
    public required string? NameProduct {get ; set ;}
    public required int Quantity {get ; set ;}
    public required string? Unity {get; set ;} = string.Empty;
}