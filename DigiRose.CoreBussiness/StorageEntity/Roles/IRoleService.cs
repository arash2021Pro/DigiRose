namespace DigiRose.CoreBussiness.StorageEntity.Roles;

public interface IRoleService
{
    Task AddNewRoleAsync(Role role);
}