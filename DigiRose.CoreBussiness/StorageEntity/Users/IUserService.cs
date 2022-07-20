namespace DigiRose.CoreBussiness.StorageEntity.Users;

public interface IUserService
{
    Task AddNewUserAsync(User? user);
    Task<bool> IsPhoneExistsAsync(string Phonenumber);
    Task<User?> GetUserAsync(int userId);
    Task<User?> GetUserAsync(string Phonenumber);
    public int GetUserRoleIdAsync(string Phonenumber);
    Task<User> GetUserAsync(string password, string Phonenumber);
    public string GenerateHash(string password);
    Task<User?> SearchUserAsync(string? RefCore);
    Task<List<User?>> GetUserListAsync(string? searchValue);
    IQueryable<User?> GetQuerableUserAsync(string ? searchValue);
}