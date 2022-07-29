using DigiRose.CoreApplication.CoreManagerApplication;
using DigiRose.CoreBussiness.RepsPattern;
using DigiRose.CoreBussiness.StorageEntity.ProductCategory;
using DigiRose.CoreBussiness.StorageEntity.Products;
using DigiRose.Models.Product;
using DigiRose.ModuleServices.CoreAuthenticationService;
using DigiRose.ModuleServices.FileCoreHandlerService;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace DigiRose.Controllers;

public class ProductController:Controller
{
    public IUnitOfWork Work;
    public ICoreServiceManager CoreServiceManager;
    public IMapper Mapper;
    public IWebHostEnvironment Environment;
    public IFileManager FileManager;
    public IDistributedCache DistributedCache;

    public ProductController(IUnitOfWork work, ICoreServiceManager coreServiceManager, IMapper mapper, IWebHostEnvironment environment, IFileManager fileManager, IDistributedCache distributedCache)
    {
        Work = work;
        CoreServiceManager = coreServiceManager;
        Mapper = mapper;
        Environment = environment;
        FileManager = fileManager;
        DistributedCache = distributedCache;
    }

    [Permission(1)]
    [HttpGet]
    public async Task<IActionResult> AddProduct(string ?Message,bool IsCompleted)
    {

        var model = new AddProductViewModel()
        {
            Message = Message,IsCompleted = IsCompleted
        };
        
        return View(model);
    }
    
    [Permission(1)]
    [HttpPost]
    public async Task<IActionResult> AddProduct(AddProductViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await CoreServiceManager.ProductService.IsProductExistsAsync(model.ProductName, model.Category);
            if (result)
            {
                model.IsCompleted = false;
                model.Message = "این محصول همچنان وجود دارد";
                ModelState.AddModelError(nameof(model.ProductName),model.Message);
                return View(model);
            }

            var extention = Guid.NewGuid().ToString().Replace("-", "").Substring(1, 6);
            var product = new Product()
            {
                ProductName = model.ProductName,
                Count = model.Count,
                Price = model.Price,
                IsEnough = model.IsEnough,
                Category = model.Category,
                filename = extention
            };
            await CoreServiceManager.ProductService.AddNewProductAsync(product);
            var change = await Work.SaveChangesAsync();
            if (change > 0)
            {
                await FileManager.UploadImageAsync(Environment,"Images","Product",extention,model.File);
                model.IsCompleted = true;
                model.Message = "محصول اضافه شد";
                return RedirectToAction("AddProduct", "Product",
                    new {Message = model.Message,IsCompleted = model.IsCompleted});
            }
            model.IsCompleted = false;
            model.Message = "خطا در ثبت محصول";
            return View(model);
        }
        return View(model);
    }


}