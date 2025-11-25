

using UsersApp.Domain.Entities.Companies;

namespace UsersApp.Domain.Repositories
{
    public interface ICompanyRepository
    {
        Task<Company?> GetByIdAsync(int id);
        Task<bool> NameExistsAsync(string name);
        Task AddAsync(Company company);
    }
}
