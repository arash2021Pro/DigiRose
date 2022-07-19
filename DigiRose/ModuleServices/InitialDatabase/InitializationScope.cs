namespace DigiRose.ModuleServices.InitialDatabase;

public static class InitializationScope
{
    public static void RunInitialScope(this IApplicationBuilder app)
    {
        var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
        using (var scope=scopeFactory.CreateScope())
        {
            var databaseInitializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
            databaseInitializer.SeedData();
        }
    }
}