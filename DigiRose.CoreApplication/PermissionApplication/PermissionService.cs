using DigiRose.CoreBussiness.RepsPattern;
using DigiRose.CoreBussiness.StorageEntity;
using Microsoft.EntityFrameworkCore;

namespace DigiRose.CoreApplication.PermissionApplication;

public class PermissionService:IPermissionService
{
    public DbSet<Permission> Permissions;
    public PermissionService(IUnitOfWork work)
    {
        Permissions = work.Set<Permission>();
    }

    public async Task AddNewPermissionAsync(Permission permission) => await Permissions.AddAsync(permission);
}