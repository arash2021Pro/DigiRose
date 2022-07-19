using DigiRose.CoreBussiness.StorageEntity.OTP;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiRose.CoreStorage.EntityConfigurations;

public class OtpConfiguration:IEntityTypeConfiguration<Otp>
{
    public void Configure(EntityTypeBuilder<Otp> builder)
    {
        builder.HasOne(x => x.User).WithMany(x => x.Otps).HasForeignKey(x => x.UserId);
    }
}