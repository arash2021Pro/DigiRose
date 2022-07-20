using System.Security.Cryptography;
using System.Text;
using DigiRose.CoreBussiness.RepsPattern;
using DigiRose.CoreBussiness.StorageEntity;
using DigiRose.CoreBussiness.StorageEntity.Users;
using Microsoft.EntityFrameworkCore;

namespace DigiRose.CoreApplication.UserApplication;

public class UserService : IUserService
{
    public DbSet<User?> Users;

    public UserService(IUnitOfWork work)
    {
        Users = work.Set<User>();
    }

    public async Task AddNewUserAsync(User? user)
    {
        user.Password = GenerateHash(user.Password);
        await Users.AddAsync(user);
    }

    public async Task<bool> IsPhoneExistsAsync(string Phonenumber) =>
        await Users.AnyAsync(x => x.Phonenumber == Phonenumber);

    public async Task<User?> GetUserAsync(int userId) =>
        await Users.AsTracking().FirstOrDefaultAsync(x => x.Id == userId);

    public async Task<User?> GetUserAsync(string Phonenumber) =>
        await Users.AsTracking().FirstOrDefaultAsync(x => x.Phonenumber == Phonenumber);

    public int GetUserRoleIdAsync(string Phonenumber) => Users.FirstOrDefault(x => x.Phonenumber == Phonenumber).RoleId;


    public async Task<User> GetUserAsync(string password, string Phonenumber)
    {
        var EncriptedPassword = GenerateHash(password);
        return await Users.AsTracking().Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Phonenumber == Phonenumber && x.Password == EncriptedPassword);
    }

    public string GenerateHash(string password)
    {
        if (String.IsNullOrEmpty(password))
            return "";
        using (var sha = new SHA512Managed())
        {
            var bytes = Encoding.ASCII.GetBytes(password);
            var encripted = sha.ComputeHash(bytes);
            return Encoding.ASCII.GetString(encripted);
        }
    }

    public async Task<User?> SearchUserAsync(string? RefCode) =>
        await Users.FirstOrDefaultAsync(x => x.ReferralSerial == RefCode);

    public async Task<List<User?>> GetUserListAsync(string? searchValue) => String.IsNullOrEmpty(searchValue)
        ? await Users.AsQueryable().OrderByDescending(x => x.Id).ToListAsync()
        : await Users.AsQueryable().Where(x => x.Phonenumber == searchValue).ToListAsync();

    public IQueryable<User?> GetQuerableUserAsync(string ? searchValue)
    {
        var users = from user in Users
            select user;
        if (!String.IsNullOrEmpty(searchValue))
        {
            users = users.Where(x => x.Phonenumber == searchValue);
        }
        return users.OrderByDescending(x=>x.Id);
    }
}