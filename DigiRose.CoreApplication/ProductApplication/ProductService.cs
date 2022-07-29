using DigiRose.CoreBussiness.RepsPattern;
using DigiRose.CoreBussiness.StorageEntity.ProductCategory;
using DigiRose.CoreBussiness.StorageEntity.Products;
using Microsoft.EntityFrameworkCore;

namespace DigiRose.CoreApplication.ProductApplication;

public class ProductService:IProductService
{
    public DbSet<Product> Products;
    public ProductService(IUnitOfWork work)
    {
        Products = work.Set<Product>();
    }
    
    public async Task AddNewProductAsync(Product product) => await Products.AddAsync(product);
    
    public async Task<bool> IsProductExistsAsync(string Productname, Category category) =>
        await Products.AnyAsync(x => x.ProductName == Productname && x.Category == category);
    
}