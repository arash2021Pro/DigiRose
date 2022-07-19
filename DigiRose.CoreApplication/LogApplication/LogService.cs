using DigiRose.CoreBussiness.RepsPattern;
using DigiRose.CoreBussiness.StorageEntity.Logging;
using Microsoft.EntityFrameworkCore;

namespace DigiRose.CoreApplication.LogApplication;

public class LogService:ILogService
{
    public DbSet<Log> Logs;
    public LogService(IUnitOfWork work)
    {
        Logs = work.Set<Log>();
    }

    public async Task AddNewLogAsync(Log log) => await Logs.AddAsync(log);


}