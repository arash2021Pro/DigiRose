namespace DigiRose.Models.Admin;

public static class UserRole
{
    public static string GetUserRoleName(this int roleId)
    {
        if (roleId == 3)
            return "ادمین";
        return "کاربر";
    }
}