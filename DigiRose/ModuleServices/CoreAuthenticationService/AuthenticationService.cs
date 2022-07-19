using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DigiRose.ModuleServices.CoreAuthenticationService;

public static class AuthenticationService
{
    public static void RunAuthentication(this IServiceCollection service)
    {
        service.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        }).AddCookie(option =>
        {
            option.LoginPath = "/User/Login";
            option.LogoutPath = "/User/Logout";
            option.ExpireTimeSpan = TimeSpan.FromMinutes(15);
            option.SlidingExpiration = true;
        });
    }
}