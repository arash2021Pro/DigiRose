using DigiRose.CoreBussiness.CoreEntity;
using DigiRose.CoreBussiness.StorageEntity.OTP;
using DigiRose.CoreBussiness.StorageEntity.Roles;
using DigiRose.CoreBussiness.StorageEntity.ShoppingCart;

namespace DigiRose.CoreBussiness.StorageEntity.Users;

public class User:Core
{
    private string GenerateRefSerial() => Guid.NewGuid().ToString().Replace("-", "").Substring(1, 6);

    public User()
    {
        ReferralSerial = GenerateRefSerial();
    }
    public string Phonenumber { get; set; }
    public string Password { get; set; }
    public UserStatus UserStatus { get; set; }
    public string ReferralSerial { get; set; }
    public int ReferralCount { get; set; }
    public bool IsObedient { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public ICollection<ShopCart>ShopCarts { get; set; }
    public ICollection<Otp>Otps { get; set; }
}