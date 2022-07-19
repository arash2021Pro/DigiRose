using DigiRose.CoreBussiness.RepsPattern;
using DigiRose.CoreBussiness.StorageEntity.Roles;
using Microsoft.EntityFrameworkCore;

namespace DigiRose.CoreApplication.RoleApplication;

public class RoleService:IRoleService
{
    public DbSet<Role> Roles;

    public RoleService(IUnitOfWork work)
    {
        Roles = work.Set<Role>();
    }

    public async Task AddNewRoleAsync(Role role) => await Roles.AddAsync(role);


}