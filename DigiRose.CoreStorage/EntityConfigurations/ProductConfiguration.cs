using DigiRose.CoreBussiness.StorageEntity.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiRose.CoreStorage.EntityConfigurations;

public class ProductConfiguration:IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.ProductName).IsRequired(false);
        builder.Property(x => x.filename).IsRequired(false);
    }
}