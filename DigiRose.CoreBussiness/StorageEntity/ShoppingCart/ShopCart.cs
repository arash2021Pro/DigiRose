using DigiRose.CoreBussiness.CoreEntity;
using DigiRose.CoreBussiness.StorageEntity.Products;
using DigiRose.CoreBussiness.StorageEntity.Users;

namespace DigiRose.CoreBussiness.StorageEntity.ShoppingCart;

public class ShopCart:Core
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}