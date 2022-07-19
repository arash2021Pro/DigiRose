using DigiRose.CoreBussiness.RepsPattern;
using DigiRose.CoreBussiness.StorageEntity.Roles;
using DigiRose.CoreBussiness.StorageEntity.Users;
using Microsoft.EntityFrameworkCore;

namespace DigiRose.ModuleServices.InitialDatabase;

public class InitializationService:IDatabaseInitializer
{
    public IUnitOfWork Work;
    public InitializationService(IUnitOfWork work)
    {
        Work = work;
    }
    public async void SeedData()
    {
        var users = Work.Set<User>();
        var roles = Work.Set<Role>();
        if (!await users.AnyAsync())
        {
            var role = new Role() {Rolename = "admin"};

            var user = new User() {Role = role};
            await roles.AddAsync(role);
            await users.AddAsync(user);
            await Work.SaveChangesAsync();
        } 
    }
}