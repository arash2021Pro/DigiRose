using DigiRose.CoreBussiness.CoreEntity;
using DigiRose.CoreBussiness.StorageEntity.RolePermissions;

namespace DigiRose.CoreBussiness.StorageEntity;

public class Permission:Core
{
    public string? PermissionName { get; set; }
    public ICollection<RolePermission>RolePermissions { get; set; }
}