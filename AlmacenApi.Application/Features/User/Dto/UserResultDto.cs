namespace AlmacenApi.Aplication.Features.User.Dto;
public class UserResultDto
{
    public required string? id{get;set;}
    public required string? FullName {get; set ;}
    public required string? UserName{get; set ;}
    public required DateTime? CreatedAt{get;set;}
    public required DateTime? UpdatedAt{get;set;}
    public UserResultDto()
    {
        this.id = null;
        this.UserName = null;
        this.CreatedAt = null;
        this.UpdatedAt = null;
        this.FullName = null;
    }
}