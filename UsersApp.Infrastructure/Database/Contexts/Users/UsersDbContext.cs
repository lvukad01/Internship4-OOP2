using Microsoft.EntityFrameworkCore;
using UsersApp.Domain.Entities.Users;
using UsersApp.Infrastructure.Database.Configurations;
using UsersApp.Infrastructure.Database.Configurations.Users;

namespace UsersApp.Infrastructure.Database.Contexts.Users
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Shemas.Default);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);

        }
    }
}
