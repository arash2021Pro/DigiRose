using System.ComponentModel;
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
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Log = DigiRose.CoreBussiness.StorageEntity.Logging.Log;
using UserStatus = DigiRose.CoreBussiness.StorageEntity.Users.UserStatus;


namespace DigiRose.Controllers;

public class AdminController:Controller
{
    public ICoreServiceManager CoreServiceManager;
    public IUnitOfWork Work;
    public IMapper Mapper;
    
    public IWebHostEnvironment _environment;
    public IFileManager FileManager;
    public IDistributedCache DistributedCache;

    public AdminController(ICoreServiceManager coreServiceManager, IUnitOfWork work, IMapper mapper, IWebHostEnvironment environment, IFileManager fileManager, IDistributedCache distributedCache)
    {
        CoreServiceManager = coreServiceManager;
        Work = work;
        Mapper = mapper;
       
        _environment = environment;
        FileManager = fileManager;
        DistributedCache = distributedCache;
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