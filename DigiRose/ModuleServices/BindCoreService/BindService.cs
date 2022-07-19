using DigiRose.CoreStructure.MessageService;

namespace DigiRose.ModuleServices.BindCoreService;

public static class BindService
{
    public static void RunBindService(this IServiceCollection service, IConfiguration configuration)
    {
        service.Configure<MessageOption>(x => configuration.GetSection("KaveNegar:ApiKey").Bind(x));
    }
}