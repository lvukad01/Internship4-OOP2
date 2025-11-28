using Microsoft.EntityFrameworkCore;
using UsersApp.Domain.Entities.Companies;
using UsersApp.Infrastructure.Database.Configurations;
using UsersApp.Infrastructure.Database.Configurations.Users;

namespace UsersApp.Infrastructure.Database.Contexts.Companies
{
    public class CompaniesDbContext : DbContext
    {
        public CompaniesDbContext(DbContextOptions<CompaniesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Shemas.Default);
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            base.OnModelCreating(modelBuilder);

        }
    }

}
