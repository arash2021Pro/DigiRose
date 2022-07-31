using System.ComponentModel;
using System.Web;
using DigiRose.CoreApplication.CoreManagerApplication;
using DigiRose.CoreBussiness.RepsPattern;
using DigiRose.CoreBussiness.StorageEntity.Users;
using DigiRose.Models.Admin;
using DigiRose.ModuleServices.CoreAuthenticationService;
using DigiRose.ModuleServices.FileCoreHandlerService;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using UserStatus = DigiRose.Models.Admin.UserStatus;

namespace DigiRose.Controllers;

public class UserController:Controller
{
    public IUnitOfWork Work;
    public ICoreServiceManager CoreServiceManager;
    public IMapper Mapper;
    public IWebHostEnvironment _environment;
    public IFileManager FileManager;
    public IDistributedCache DistributedCache;

    public UserController(IUnitOfWork work, ICoreServiceManager coreServiceManager, IMapper mapper, IFileManager fileManager, IDistributedCache distributedCache)
    {
        Work = work;
        CoreServiceManager = coreServiceManager;
        Mapper = mapper;
        FileManager = fileManager;
        DistributedCache = distributedCache;
    }
    [Permission(1)]
    [Description("The Sqlcache successfully working to store data but it dosen support the PaginatedList type to fetch data well .")]
    [ResponseCache(CacheProfileName = "UserDefault")]
    [HttpGet]
    public async Task<IActionResult> UserDataTable(string? searchValue,int? pageNumber,string sortOrder)
    {
        /*var cacheOptions = new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
        };
        var CachedUsersJson = await DistributedCache.GetAsync("Users");
        if (CachedUsersJson == null)
        {
            var pageSize = 4;
            var User = CoreServiceManager.UserService.GetQuerableUserAsync(searchValue,sortOrder);
            if (!String.IsNullOrEmpty(searchValue)) pageNumber = 1;
            var users = await PaginatedList<User>.CreateAsync(User, pageNumber ?? 1, pageSize);
            var JsonUsers = JsonConvert.SerializeObject(users);
            await DistributedCache.SetStringAsync("Users",JsonUsers);
            return View(users);
        }
        var cachedUsers = JsonSerializer.Deserialize<PaginatedList<User>>(CachedUsersJson);  //JsonSerializer.Deserialize<PaginatedList<User>>(CachedUsersJson);
        return View(cachedUsers);*/
        
        var pageSize = 4;
        var User = CoreServiceManager.UserService.GetQuerableUserAsync(searchValue,sortOrder);
        if (!String.IsNullOrEmpty(searchValue)) pageNumber = 1;
        var users = await PaginatedList<User>.CreateAsync(User, pageNumber ?? 1, pageSize);
        return View(users);
        
    }
    [Permission(1)]
    [HttpGet]
    public async Task<IActionResult> ActivateUser(int Id)
    {
        var user = await CoreServiceManager.UserService.GetUserAsync(Id);
        user.UserStatus = CoreBussiness.StorageEntity.Users.UserStatus.Active;
        var change = await Work.SaveChangesAsync();
        if (change > 0)
        {
            return RedirectToAction("UserDataTable", "User");
        }
        return RedirectToAction("Dashboard","Admin");
    }
    [Permission(1)]
    [HttpGet]
    public async Task<IActionResult> DeactivateUser(int Id)
    {
        var user = await CoreServiceManager.UserService.GetUserAsync(Id);
        user.UserStatus = CoreBussiness.StorageEntity.Users.UserStatus.Inactive;
        var change = await Work.SaveChangesAsync();
        if (change > 0)
        {
            return RedirectToAction("UserDataTable", "User");
        }
        return RedirectToAction("Dashboard","Admin");
    }
    [Permission(1)]
    [HttpGet]
    public async Task<IActionResult> EditUserPhone(int Id)
    {
        var user = await CoreServiceManager.UserService.GetUserAsync(Id);
        var data = user.Adapt<EditUserPhoneViewModel>();
        return View(data);
    }
    [Permission(1)]
    [HttpPost]
    public async Task<IActionResult> EditUserPhone(EditUserPhoneViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await CoreServiceManager.UserService.GetUserAsync(model.Id);
            user.Phonenumber = model.Phonenumber;
            user.ModificationTime = DateTime.Now.ToShortTimeString();
            var change = await Work.SaveChangesAsync();
            if (change > 0)
            {
                model.IsCompleted = true;
                return RedirectToAction("UserDataTable", "User");
            }
            model.IsCompleted = false;
            model.Message = "خطا در پاسخگویی";
            return View(model);
        }
        return View(model);
    }
    
    
    
    
    
}