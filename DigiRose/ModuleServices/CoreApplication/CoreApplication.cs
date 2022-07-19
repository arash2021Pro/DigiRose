using DigiRose.CoreApplication.CoreManagerApplication;
using DigiRose.CoreApplication.LogApplication;
using DigiRose.CoreApplication.OtpApplication;
using DigiRose.CoreApplication.PermissionApplication;
using DigiRose.CoreApplication.RoleApplication;
using DigiRose.CoreApplication.RolePermissionApplication;
using DigiRose.CoreApplication.UserApplication;
using DigiRose.CoreBussiness.RepsPattern;
using DigiRose.CoreBussiness.StorageEntity;
using DigiRose.CoreBussiness.StorageEntity.Logging;
using DigiRose.CoreBussiness.StorageEntity.OTP;
using DigiRose.CoreBussiness.StorageEntity.RolePermissions;
using DigiRose.CoreBussiness.StorageEntity.Roles;
using DigiRose.CoreBussiness.StorageEntity.Users;
using DigiRose.CoreStorage.SqlContext;
using DigiRose.CoreStructure.MessageService;
using Hangfire.Logging;
using MapsterMapper;

namespace DigiRose.ModuleServices.CoreApplication;

public static class CoreApplication
{
    public static void RunCoreApplication(this IServiceCollection service)
    {
        service.AddScoped<IUnitOfWork, ApplicationContext>();
        service.AddScoped<IMessageService, MessageService>();
        service.AddScoped<IUserService, UserService>();
        service.AddScoped<ICoreServiceManager, CoreServiceManager>();
        service.AddScoped<IMapper, Mapper>();
        service.AddScoped<IRoleService, RoleService>();
        service.AddScoped<IOtpService, OtpService>();
        service.AddScoped<IPermissionService, PermissionService>();
        service.AddScoped<IRolePermissionService, RolePermissionService>();
        service.AddScoped<ILogService, LogService>();
    }
}