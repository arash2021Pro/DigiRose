using Serilog;

namespace DigiRose.ModuleServices.SerilogCoreService;

public static class SerilogService
{
    public static void RunSerilogService(this IHostBuilder hostBuilder, IConfiguration configuration)
    {
        string SerilogUrl = configuration.GetConnectionString(("Serilog"));
        hostBuilder.UseSerilog((context, config) =>
        {
            config.WriteTo.MSSqlServer(SerilogUrl, "Logs", autoCreateSqlTable: true).MinimumLevel.Information();
        });
    } 
}