using ElmahCore;
using ElmahCore.Mvc;
using ElmahCore.Sql;

namespace DigiRose.ModuleServices.ElmahCoreService;

public static class ElmahService
{
    public static void RunElmahService(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddElmah<SqlErrorLog>(options =>
        {
            options.Path = "/Elmah";
            options.ConnectionString = configuration.GetConnectionString("elmah");
            options.Filters.Add(new NotFoundFilter());
        });
    }
    public class NotFoundFilter:ElmahCore.IErrorFilter
    {
        public void OnErrorModuleFiltering(object sender, ExceptionFilterEventArgs args)
        {
            if (args.Exception.GetBaseException() is FileNotFoundException)
            {
                args.Dismiss();
            }
            
            if(args.Context is HttpContext httpContext)
                if (httpContext.Response.StatusCode == 404)
                    args.Dismiss();
        }
    }
}