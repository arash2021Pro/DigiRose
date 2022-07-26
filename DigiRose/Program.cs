using DigiRose.CoreStorage.SqlContext;
using DigiRose.ModuleServices.BindCoreService;
using DigiRose.ModuleServices.CoreApplication;
using DigiRose.ModuleServices.CoreAuthenticationService;
using DigiRose.ModuleServices.ElmahCoreService;
using DigiRose.ModuleServices.HangfireCoreServices;
using DigiRose.ModuleServices.InitialDatabase;
using DigiRose.ModuleServices.SerilogCoreService;
using DigiRose.ModuleServices.SqlCacheCoreService;
using DigiRose.ModuleServices.SqlServerServices;
using ElmahCore.Mvc;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(x =>
{
    x.CacheProfiles.Add("UserDefault",new CacheProfile()
    {
        Duration = 5,
        NoStore = true,
        Location = ResponseCacheLocation.Any
    });
});
builder.Services.RunCoreApplication();
builder.Services.RunSqlService(builder.Configuration);
builder.Services.RunHangfireService(builder.Configuration);
builder.Services.RunElmahService(builder.Configuration);
builder.Host.RunSerilogService(builder.Configuration);
builder.Services.AddHealthChecks().AddDbContextCheck<ApplicationContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.RunBindService(builder.Configuration);
builder.Services.RunAuthentication();
builder.Services.AddResponseCompression(option => { option.EnableForHttps = true; });
builder.Services.AddResponseCaching();
builder.Services.RunSqlCacheService(builder.Configuration);
var app = builder.Build();
//app.RunInitialScope();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseResponseCaching();
app.UseHangfireDashboard();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseElmah();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();