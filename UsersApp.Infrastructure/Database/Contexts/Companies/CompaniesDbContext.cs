using Microsoft.EntityFrameworkCore;
using UsersApp.Domain.Entities.Companies;
using UsersApp.Infrastructure.Database.Configurations;


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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompaniesDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

        }
    }

}
