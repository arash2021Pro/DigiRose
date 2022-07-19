using DigiRose.CoreBussiness.RepsPattern;
using DigiRose.CoreBussiness.StorageEntity.RolePermissions;
using DigiRose.CoreBussiness.StorageEntity.Roles;
using Microsoft.EntityFrameworkCore;

namespace DigiRose.CoreApplication.RolePermissionApplication;

public class RolePermissionService:IRolePermissionService
{
    public DbSet<RolePermission> RolePermissions;
    public RolePermissionService(IUnitOfWork work)
    {
        RolePermissions = work.Set<RolePermission>();
    }

    public async Task AddNewRolePermission(RolePermission rolePermission) =>
        await RolePermissions.AddAsync(rolePermission);

    public async Task<bool> HasPermissionAsync(int roleId, int permissionId) =>
        await RolePermissions.AnyAsync(x => x.RoleId == roleId && x.PermissionId == permissionId && !x.IsDeleted);
    


}