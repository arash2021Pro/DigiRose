using DigiRose.CoreApplication.CoreManagerApplication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DigiRose.ModuleServices.CoreAuthenticationService;

public class PermissionAttribute:AuthorizeAttribute,IAsyncAuthorizationFilter
{
    private int permissionId;
    public PermissionAttribute(int permissionId)
    {
        this.permissionId = permissionId;
    }
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            var Phonenumber = context.HttpContext.User.Identity.Name;
            var CoreServiceManager = context.HttpContext.RequestServices.GetRequiredService<ICoreServiceManager>();
            var RoleId = CoreServiceManager.UserService.GetUserRoleIdAsync(Phonenumber);
            if (!await CoreServiceManager.RolePermissionService.HasPermissionAsync(RoleId, permissionId))
            {
                context.Result = new RedirectResult("/User/Login");
            }
        }
        else
        {
            context.Result = new RedirectResult("/User/Login");
        }
       
    }
}