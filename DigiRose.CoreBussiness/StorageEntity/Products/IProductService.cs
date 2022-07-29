using DigiRose.CoreBussiness.StorageEntity.ProductCategory;

namespace DigiRose.CoreBussiness.StorageEntity.Products;

public interface IProductService
{
    Task AddNewProductAsync(Product product);
    Task<bool> IsProductExistsAsync(string Productname, Category category);
}