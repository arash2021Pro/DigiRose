using DigiRose.CoreBussiness.CoreEntity;

namespace DigiRose.CoreBussiness.StorageEntity.Logging;

public class Log:Core
{
    public string?BrowserName { get; set; }
    public string? Username { get; set; }
    public int UserId { get; set; }
    public string ?UrlAction { get; set; }
}