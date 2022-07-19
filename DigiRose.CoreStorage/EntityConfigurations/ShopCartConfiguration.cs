using DigiRose.CoreBussiness.StorageEntity.ShoppingCart;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiRose.CoreStorage.EntityConfigurations;

public class ShopCartConfiguration:IEntityTypeConfiguration<ShopCart>
{
    public void Configure(EntityTypeBuilder<ShopCart> builder)
    {
        builder.HasOne(x => x.Product).WithMany(x => x.ShopCarts).HasForeignKey(x => x.ProductId);
        builder.HasOne(x => x.User).WithMany(x => x.ShopCarts).HasForeignKey(x => x.UserId);
    }
}