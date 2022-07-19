using DigiRose.CoreBussiness.CoreEntity;
using DigiRose.CoreBussiness.StorageEntity.ProductCategory;
using DigiRose.CoreBussiness.StorageEntity.ShoppingCart;

namespace DigiRose.CoreBussiness.StorageEntity.Products;

public class Product:Core
{
    public string? ProductName { get; set; }
    public int Price { get; set; }
    public int Count { get; set; }
    public bool IsEnough { get; set; }
    public Category Category { get; set; }
    public ICollection<ShopCart>ShopCarts { get; set; }
}