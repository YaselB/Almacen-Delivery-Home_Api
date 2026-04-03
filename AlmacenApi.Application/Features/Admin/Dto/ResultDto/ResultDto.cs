namespace AlmacenApi.Aplication.Features.Dto.ResultDto;
public class ResultAdminDto
{
    public required string? id{get;set ;}
    public required string? UserName {get; set ;}
    public required string? FullName {get; set ;}
    public required DateTime? CreatedAt {get; set ;}
    public required DateTime? UpdatedAt {get; set;}
    public ResultAdminDto()
    {
        this.CreatedAt = null;
        this.UpdatedAt = null;
        this.UserName = null;
        this.id = null;
        this.FullName = null;
    }
}