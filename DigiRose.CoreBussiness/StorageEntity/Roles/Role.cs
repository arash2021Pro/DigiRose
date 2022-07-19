using DigiRose.CoreBussiness.CoreEntity;
using DigiRose.CoreBussiness.StorageEntity.RolePermissions;
using DigiRose.CoreBussiness.StorageEntity.Users;

namespace DigiRose.CoreBussiness.StorageEntity.Roles;

public class Role:Core
{
    public string? Rolename { get; set; }
    public ICollection<User>Users { get; set; }
    public ICollection<RolePermission>RolePermissions { get; set; }
}