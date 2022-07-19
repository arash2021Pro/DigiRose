using DigiRose.CoreApplication.CoreManagerApplication;
using DigiRose.CoreBussiness.RepsPattern;
using DigiRose.CoreBussiness.StorageEntity.Logging;
using DigiRose.CoreBussiness.StorageEntity.Users;
using DigiRose.CoreStorage.Migrations;
using DigiRose.ModuleServices.CoreAuthenticationService;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Log = DigiRose.CoreBussiness.StorageEntity.Logging.Log;


namespace DigiRose.Controllers;

public class AdminController:Controller
{
    public ICoreServiceManager CoreServiceManager;
    public IUnitOfWork Work;
    public IMapper Mapper;

    public AdminController(ICoreServiceManager coreServiceManager, IUnitOfWork work, IMapper mapper)
    {
        CoreServiceManager = coreServiceManager;
        Work = work;
        Mapper = mapper;
    }
    [HttpGet]
    [Permission(1)]
    public async Task<IActionResult> Dashboard()
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

    [HttpGet]
    public async Task<IActionResult> UserDataTable(string? searchValue)
    {
        var users = await CoreServiceManager.UserService.GetUserListAsync(searchValue);
        return View(users);
    }

    [HttpGet]
    public async Task<IActionResult> ActivateUser(int Id)
    {
        var user = await CoreServiceManager.UserService.GetUserAsync(Id);
        user.UserStatus = UserStatus.Active;
        var change = await Work.SaveChangesAsync();
        if (change > 0)
        {
            return RedirectToAction("UserDataTable", "Admin");
        }
        return RedirectToAction("Dashboard","Admin");
    }

    [HttpGet]
    public async Task<IActionResult> DeactivateUser(int? id)
    {
        var user = await CoreServiceManager.UserService.GetUserAsync(id.Value);
        user.UserStatus = UserStatus.Inactive;
        var change = await Work.SaveChangesAsync();
        if (change > 0)
        {
            return RedirectToAction("UserDataTable", "Admin");
        }
        return RedirectToAction("Dashboard","Admin");
    }
    // Pagination
}