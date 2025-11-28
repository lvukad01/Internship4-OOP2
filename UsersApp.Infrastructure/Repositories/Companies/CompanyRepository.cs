

using Microsoft.EntityFrameworkCore;
using UsersApp.Domain.Entities.Companies;
using UsersApp.Domain.Repositories;
using UsersApp.Infrastructure.Database.Contexts.Companies;

namespace UsersApp.Infrastructure.Repositories.Companies
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompaniesDbContext _dbContext;

        public CompanyRepository(CompaniesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Company?> GetByIdAsync(int id)
        {
            return await _dbContext.Companies.FindAsync(id);
        }

        public async Task<Company?> GetByNameAsync(string name)
        {
            return await _dbContext.Companies
                .FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<bool> NameExistsAsync(string name)
        {
            return await _dbContext.Companies.AnyAsync(c => c.Name == name);
        }

        public async Task AddAsync(Company company)
        {
            await _dbContext.Companies.AddAsync(company);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Company company)
        {
            _dbContext.Companies.Update(company);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var company = await _dbContext.Companies.FindAsync(id);
            if (company != null)
            {
                _dbContext.Companies.Remove(company);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Company?> GetByIdWithAuthAsync(int id, string username, string password)
        {
            return await GetByIdAsync(id);
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _dbContext.Companies.ToListAsync();
        }

        public async Task<IEnumerable<Company>> GetAllWithAuthAsync(string username, string password)
        {
            return await GetAllAsync();
        }

    }
}
