namespace AlmacenApi.Aplication.Features.Combo.Dto;
public class ProductcomboResultDto
{
    public required string? id {get ; set ;}
    public required string? NameProduct {get ; set ;}
    public required double Quantity {get ; set ;}
    public required string? Unity {get; set ;} = string.Empty;
}