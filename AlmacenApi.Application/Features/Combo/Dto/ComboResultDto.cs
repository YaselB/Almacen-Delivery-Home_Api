namespace AlmacenApi.Aplication.Features.Combo.Dto;
public class ComboResultDto
{
    public required string id {get ; set ;} = string.Empty;
    public required string Name {get ; set ;} = string.Empty;
    public required List<ProductcomboResultDto> Products {get ; set ;} = new List<ProductcomboResultDto>();
    public required string? NameUserOrAdmin {get ; set ;}
}