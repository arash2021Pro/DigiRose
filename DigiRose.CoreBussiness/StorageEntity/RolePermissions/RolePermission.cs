using DigiRose.CoreBussiness.CoreEntity;
using DigiRose.CoreBussiness.StorageEntity.Roles;

namespace DigiRose.CoreBussiness.StorageEntity.RolePermissions;

public class RolePermission:Core
{
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public int PermissionId { get; set; }
    public Permission Permission { get; set; }
}