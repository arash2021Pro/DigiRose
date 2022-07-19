namespace DigiRose.CoreBussiness.StorageEntity.Logging;

public interface ILogService
{
    Task AddNewLogAsync(Log log);
}