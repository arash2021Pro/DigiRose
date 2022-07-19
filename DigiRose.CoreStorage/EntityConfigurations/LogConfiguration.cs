using DigiRose.CoreBussiness.StorageEntity.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiRose.CoreStorage.EntityConfigurations;

public class LogConfiguration:IEntityTypeConfiguration<Log>
{
    public void Configure(EntityTypeBuilder<Log> builder)
    {
        builder.Property(x => x.Username).HasMaxLength(11).IsRequired(false);
        builder.Property(x => x.BrowserName).IsRequired(false);
        builder.Property(x => x.UrlAction).IsRequired(false);
    }
}