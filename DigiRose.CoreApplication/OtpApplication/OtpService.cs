using DigiRose.CoreBussiness.RepsPattern;
using DigiRose.CoreBussiness.StorageEntity.OTP;
using Microsoft.EntityFrameworkCore;

namespace DigiRose.CoreApplication.OtpApplication;

public class OtpService:IOtpService
{
    private DbSet<Otp?> Otps;
    public OtpService(IUnitOfWork work)
    {
        Otps = work.Set<Otp>();
    }

    public async Task AddNewOtpAsync(Otp? otp) => await Otps.AddAsync(otp);
    public async Task<Otp?> GetOtpAsync(string code) => await Otps.AsTracking().FirstOrDefaultAsync(x => x.code == code);
    
    public string GenerateCode(int len) { var code = ""; var Randome = new Random(); for (int i = 1; i <= len; i++) { code += Randome.Next(1, 10); } return code; }


}