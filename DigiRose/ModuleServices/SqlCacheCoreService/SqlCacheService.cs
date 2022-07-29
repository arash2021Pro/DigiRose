namespace DigiRose.ModuleServices.SqlCacheCoreService;

public static class SqlCacheService
{
    public static void RunSqlCacheService(this IServiceCollection service,IConfiguration configuration)
    {
        var SqlCacheConnetion = configuration.GetConnectionString("UsersCache");
        service.AddDistributedSqlServerCache(option =>
        {
            option.ConnectionString = SqlCacheConnetion;
            option.SchemaName = "dbo";
            option.TableName = "UsersCache";
        });
    }
}