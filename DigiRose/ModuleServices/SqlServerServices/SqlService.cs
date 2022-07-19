using DigiRose.CoreStorage.SqlContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DigiRose.ModuleServices.SqlServerServices;

public static class SqlService
{
    public static void RunSqlService(this IServiceCollection service, IConfiguration configuration)
    {
        var StorageUrl = configuration.GetConnectionString("DefaultConnection");
        service.AddDbContextPool<ApplicationContext>(option =>
        {
            option.UseSqlServer(StorageUrl, x =>
            {
                x.EnableRetryOnFailure(3);
                x.MinBatchSize(5).MaxBatchSize(50);
                x.UseNodaTime();
            }).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            option.AddInterceptors();
            option.LogTo(Console.WriteLine);
        });
    }
}