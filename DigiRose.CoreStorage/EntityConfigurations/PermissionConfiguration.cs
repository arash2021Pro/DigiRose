using DigiRose.CoreBussiness.StorageEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiRose.CoreStorage.EntityConfigurations;

public class PermissionConfiguration:IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.Property(x => x.PermissionName).IsRequired(false);
    }
}