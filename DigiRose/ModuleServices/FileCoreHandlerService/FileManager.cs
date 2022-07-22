namespace DigiRose.ModuleServices.FileCoreHandlerService;

public class FileManager:IFileManager
{
    public async Task<(bool IsDone,string Message)> UploadImageAsync(IWebHostEnvironment environment,string parentfile,string childfile,IFormFile file,int userId)
        {
            if (file.Length == 0 || file == null)
            {
                return (false, "فایل موجود نیست");
            }
            var filename =  userId + Path.GetExtension(file.FileName) ;
            var filepath = Path.Combine(environment.WebRootPath,$"{parentfile+"/"+childfile}",filename);
            await using (var filestream = new FileStream(filepath,FileMode.Create))
            {
                await file.CopyToAsync(filestream);
            }
            return (true, "موفق");
        }
        public async Task<byte[]> DownloadImageAsync(IWebHostEnvironment environment, string parentfile, string childfile,  int userId)
        {
            var path = Path.Combine(environment.WebRootPath, $"{parentfile + "/" + childfile}", userId + ".png");
            FileInfo fileInfo = new FileInfo(path);
            byte[] data = new byte[fileInfo.Length];
            using (FileStream fs = fileInfo.OpenRead())
            {
               await fs.ReadAsync(data, 0, data.Length);
            }
            return data;
        }

        public async Task <(bool IsDone, string Message)> UploadImageAsync(IWebHostEnvironment environment, string parentfile, string childfile, string extention, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return (false,"فایل خالی");
            var filename = extention + Path.GetExtension(file.FileName);
            var filepath = Path.Combine(environment.WebRootPath, $"{parentfile + "/" + childfile}", filename);
            
            await using var filestream = new FileStream(filepath,FileMode.Create);
            await file.CopyToAsync(filestream);
            return (true, "تامام");
        }

        public async Task<byte[]> DownloadImageAsync(IWebHostEnvironment environment, string parentfile, string childfile, string extention)
        {
            var path = Path.Combine(environment.WebRootPath, $"{parentfile + "/" + childfile}", extention + ".jpg");
            FileInfo fileInfo = new FileInfo(path);
            byte[] data = new byte[fileInfo.Length];
            using (FileStream fs = fileInfo.OpenRead())
            {
                await fs.ReadAsync(data, 0, data.Length);
            }
            return data;
        }
}