namespace DigiRose.ModuleServices.FileCoreHandlerService;

public interface IFileManager
{
    Task<(bool IsDone, string Message)> UploadImageAsync(IWebHostEnvironment environment,string parentfile,string childfile,IFormFile file,int userId);
    Task<byte[]> DownloadImageAsync(IWebHostEnvironment environment,string parentfile,string childfile,int userId);
    Task<(bool IsDone, string Message)> UploadImageAsync(IWebHostEnvironment environment,string parentfile,string childfile,string extention,IFormFile file);

    Task<byte[]> DownloadImageAsync(IWebHostEnvironment environment, string parentfile,
        string childfile, string extention);
}