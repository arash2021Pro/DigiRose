namespace DigiRose.Models.Admin;

public static class UserStatus
{
    public static string GetUserStatus(this CoreBussiness.StorageEntity.Users.UserStatus status )
    {
        switch (status)
        {
            case CoreBussiness.StorageEntity.Users.UserStatus.None:
                return "نا مشخص";
            break;
            case CoreBussiness.StorageEntity.Users.UserStatus.Active:
                return "فعال";
            break;
            case CoreBussiness.StorageEntity.Users.UserStatus.Inactive:
                return "مسدود";
        }
        return "";
    }
}