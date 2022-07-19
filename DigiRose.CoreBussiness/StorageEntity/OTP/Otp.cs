using DigiRose.CoreBussiness.CoreEntity;
using DigiRose.CoreBussiness.StorageEntity.Users;

namespace DigiRose.CoreBussiness.StorageEntity.OTP;

public class Otp:Core
{
    public const int ExpireLimit = 15;

    public Otp()
    {
       ExpireTime = DateTimeOffset.Now.AddMinutes(ExpireLimit);
    }
    public int UserId { get; set; }
    public User User { get; set; }
    public string code { get; set; }
    public bool IsUsed { get; set; }
    public DateTimeOffset ExpireTime { get; set; }
    public bool IsAuthentic => !IsUsed && DateTimeOffset.Now < ExpireTime;
}