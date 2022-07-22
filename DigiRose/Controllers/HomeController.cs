using System.Diagnostics;
using DigiRose.CoreApplication.CoreManagerApplication;
using DigiRose.CoreBussiness.RepsPattern;
using DigiRose.CoreBussiness.StorageEntity;
using DigiRose.CoreBussiness.StorageEntity.Logging;
using DigiRose.CoreBussiness.StorageEntity.RolePermissions;
using DigiRose.CoreBussiness.StorageEntity.Roles;
using DigiRose.CoreBussiness.StorageEntity.Users;
using DigiRose.ModuleServices.CoreAuthenticationService;
using Microsoft.AspNetCore.Mvc;

namespace DigiRose.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public ICoreServiceManager CoreServiceManager;
    public IUnitOfWork Work;
    public HomeController(ILogger<HomeController> logger, ICoreServiceManager coreServiceManager, IUnitOfWork work)
    {
        _logger = logger;
        CoreServiceManager = coreServiceManager;
        Work = work;
    }

    public async  Task<IActionResult> Index()
    {
        var log = new Log()
        {
            Username = HttpContext.User.Identity.Name,
            BrowserName = HttpContext.Request.Headers["user-agent"].ToString(),
            UserId = HttpContext.User.GetCurrentUserId(),
            UrlAction = HttpContext.Request.RouteValues["controller"] + "/" + HttpContext.Request.RouteValues["action"]
        };
        await CoreServiceManager.LogService.AddNewLogAsync(log);
        await Work.SaveChangesAsync();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}