using DigiRose.CoreApplication.CoreManagerApplication;
using DigiRose.CoreBussiness.RepsPattern;
using DigiRose.CoreBussiness.StorageEntity.Logging;
using DigiRose.CoreBussiness.StorageEntity.Roles;
using DigiRose.CoreBussiness.StorageEntity.Users;
using DigiRose.CoreStorage.Migrations;
using DigiRose.CoreStorage.SqlContext;
using DigiRose.Models.Admin;
using DigiRose.ModuleServices.CoreAuthenticationService;
using DigiRose.ModuleServices.FileCoreHandlerService;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Log = DigiRose.CoreBussiness.StorageEntity.Logging.Log;
using UserStatus = DigiRose.CoreBussiness.StorageEntity.Users.UserStatus;


namespace DigiRose.Controllers;

public class AdminController:Controller
{
    public ICoreServiceManager CoreServiceManager;
    public IUnitOfWork Work;
    public IMapper Mapper;
    public ApplicationContext Context;
    public IWebHostEnvironment _environment;
    public IFileManager FileManager;

    public AdminController(ICoreServiceManager coreServiceManager, IUnitOfWork work, IMapper mapper, ApplicationContext context, IWebHostEnvironment environment, IFileManager fileManager)
    {
        CoreServiceManager = coreServiceManager;
        Work = work;
        Mapper = mapper;
        Context = context;
        _environment = environment;
        FileManager = fileManager;
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
    public async Task<IActionResult> UserDataTable(string? searchValue,int? pageNumber,string sortOrder)
    {
        var pageSize = 4;
        var User = CoreServiceManager.UserService.GetQuerableUserAsync(searchValue,sortOrder);
        if (!String.IsNullOrEmpty(searchValue)) pageNumber = 1;
        return View(await PaginatedList<User>.CreateAsync(User, pageNumber ?? 1, pageSize));
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
    public async Task<IActionResult> DeactivateUser(int Id)
    {
        var user = await CoreServiceManager.UserService.GetUserAsync(Id);
        user.UserStatus = UserStatus.Inactive;
        var change = await Work.SaveChangesAsync();
        if (change > 0)
        {
            return RedirectToAction("UserDataTable", "Admin");
        }
        return RedirectToAction("Dashboard","Admin");
    }
    
    [HttpGet]
    public async Task<IActionResult> UploadLogo()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UploadLogo(LogoViewModel model)
    {
        if (ModelState.IsValid)
        {
           var result = await FileManager.UploadImageAsync(_environment, "Images", "SiteLogo", model.File, User.GetCurrentUserId());
           if (result.IsDone)
           {
               return RedirectToAction("Dashboard", "Admin");
           }
           model.IsCompleted = false;
           model.Message = "خطا در پاسخگویی";
           ModelState.AddModelError(nameof(model.File),model.Message);
        }
        return View(model);
    }

    public async Task<IActionResult> DownloadLogo()
    {
        var data = await FileManager.DownloadImageAsync(_environment, "Images", "SiteLogo", User.GetCurrentUserId());
        return File(data,"Image/png");
    }

}