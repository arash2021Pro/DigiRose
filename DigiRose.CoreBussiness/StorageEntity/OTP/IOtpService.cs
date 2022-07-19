namespace DigiRose.CoreBussiness.StorageEntity.OTP;

public interface IOtpService
{
    Task AddNewOtpAsync(Otp? otp);
    Task<Otp?> GetOtpAsync(string code);
    public string GenerateCode(int len);
}