using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AlmacenApi.Aplication.Common.Security;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequiredPermissionAtribute : Attribute , IAuthorizationFilter
{
    private readonly string permissionRequired;
    public RequiredPermissionAtribute(string PermissionRequired)
    {
        permissionRequired = PermissionRequired;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if(user?.Identity?.IsAuthenticated != true)
        {
            context.Result = new UnauthorizedObjectResult(new
            {
                error = "No autenticado",
                mensaje = "Debes iniciar sesion para acceder a este recurso"
            });
            return;
        }
        var hashPermission = user.Claims.Any(c => c.Type == "permission" && c.Value == permissionRequired);
        if (!hashPermission)
        {
            context.Result = new ObjectResult(new
            {
               error = "No tiene permiso para realizar esta acción",
               statusCode = 403 
            })
            {
                StatusCode = 403
            };
            return;
        }
    }
}