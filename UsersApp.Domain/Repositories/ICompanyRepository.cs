using UsersApp.Domain.Entities.Companies;

namespace UsersApp.Domain.Repositories
{
    public interface ICompanyRepository
    {
        Task<Company?> GetByIdAsync(int id);
        Task<IEnumerable<Company>> GetAllAsync();
        Task AddAsync(Company company);
        Task UpdateAsync(Company company);
        Task DeleteAsync(int id);
        Task<bool> NameExistsAsync(string name);
        Task<Company?> GetByIdWithAuthAsync(int id, string username, string password);
        Task<IEnumerable<Company>> GetAllWithAuthAsync(string username, string password);
        Task<Company?> GetByNameAsync(string name);
    }
}
