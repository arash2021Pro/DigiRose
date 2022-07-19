namespace DigiRose.CoreBussiness.StorageEntity.RolePermissions;

public interface IRolePermissionService
{
    Task AddNewRolePermission(RolePermission rolePermission);
    Task<bool> HasPermissionAsync(int roleId, int permissionId);
}