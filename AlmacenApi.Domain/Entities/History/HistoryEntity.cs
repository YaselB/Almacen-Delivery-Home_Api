using AlmacenApi.Domain.Common;

namespace AlmacenApi.Domain.Entities.History;
public class HistoryEntity : GenericEntity<HistoryEntity>
{
    public enum Type
    {
        Entrada,Salida,Creaciones,Modificaciones,Eliminaciones,login
    }
    public Type ActionType {get; set ;}
    public string UserOrAdminUserName{ get ; set ;} = string.Empty;
    public string Description {get ; set ;} = string.Empty;

    public static HistoryEntity Create(Type type ,string username , string Description)
    {
        var history = new HistoryEntity
        {
            ActionType = type,
            Description = Description,
            UserOrAdminUserName = username
        };
        return history;
    }
}