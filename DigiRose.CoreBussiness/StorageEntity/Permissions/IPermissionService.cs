namespace DigiRose.CoreBussiness.StorageEntity;

public interface IPermissionService
{
    Task AddNewPermissionAsync(Permission permission);
}