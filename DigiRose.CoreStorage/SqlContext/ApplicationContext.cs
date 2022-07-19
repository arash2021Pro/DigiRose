using System.Reflection;
using DigiRose.CoreBussiness.CoreEntity;
using DigiRose.CoreBussiness.RepsPattern;
using DigiRose.CoreBussiness.StorageEntity;
using DigiRose.CoreBussiness.StorageEntity.Logging;
using DigiRose.CoreBussiness.StorageEntity.OTP;
using DigiRose.CoreBussiness.StorageEntity.ProductCategory;
using DigiRose.CoreBussiness.StorageEntity.Products;
using DigiRose.CoreBussiness.StorageEntity.RolePermissions;
using DigiRose.CoreBussiness.StorageEntity.Roles;
using DigiRose.CoreBussiness.StorageEntity.ShoppingCart;
using DigiRose.CoreBussiness.StorageEntity.Users;
using DNTPersianUtils.Core;
using Microsoft.EntityFrameworkCore;

namespace DigiRose.CoreStorage.SqlContext;

public class ApplicationContext:DbContext,IUnitOfWork
{
    public ApplicationContext(DbContextOptions<ApplicationContext>options):base(options)
    {
        
    }
    
    public DbSet<User>Users { get; set; }
    public DbSet<ShopCart>ShopCarts { get; set; }
    public DbSet<Role>Roles { get; set; }
    public DbSet<RolePermission>RolePermissions { get; set; }
    public DbSet<Product>Products { get; set; }
    public DbSet<Permission>Permissions { get; set; }
    public DbSet<Otp>Otps { get; set; }
    public DbSet<Log>Logs { get; set; }




      public override DbSet<TEntity> Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

       


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
      //     modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(UserConfiguration)));

            var entities = modelBuilder
                .Model
                .GetEntityTypes()
                .Select(x => x.ClrType)
                .Where(x => x.BaseType == typeof(Core))
                .ToList();

            foreach (var type in entities)
            {
                var method = SetGlobalQueryMethod.MakeGenericMethod(type);
                method.Invoke(this, new object[] {modelBuilder});
            }
        }

        public static readonly MethodInfo SetGlobalQueryMethod = typeof(ApplicationContext)
            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQuery");

        public void SetGlobalQuery<T>(ModelBuilder builder) where T : Core
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }


        private void changeEntitiesStates()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Core && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {

                if (entityEntry.State == EntityState.Added)
                {
                    ((Core) entityEntry.Entity).CurrentTime = DateTime.Now.ToShortPersianDateTimeString();
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    ((Core) entityEntry.Entity).ModificationTime = DateTimeOffset.Now.ToShortPersianDateTimeString();
                }
            }
        }
}