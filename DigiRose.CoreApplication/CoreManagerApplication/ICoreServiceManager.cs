using DigiRose.CoreBussiness.StorageEntity;
using DigiRose.CoreBussiness.StorageEntity.Logging;
using DigiRose.CoreBussiness.StorageEntity.OTP;
using DigiRose.CoreBussiness.StorageEntity.Products;
using DigiRose.CoreBussiness.StorageEntity.RolePermissions;
using DigiRose.CoreBussiness.StorageEntity.Roles;
using DigiRose.CoreBussiness.StorageEntity.Users;

namespace DigiRose.CoreApplication.CoreManagerApplication;

public interface ICoreServiceManager
{
    public IUserService UserService { get; set; }
    public IRoleService RoleService { get; set; }
    public IOtpService OtpService { get; set; }
    public IPermissionService PermissionService { get; set; }
    public IRolePermissionService RolePermissionService { get; set; }
    public ILogService LogService { get; set; }
    public IProductService ProductService { get; set; }
}