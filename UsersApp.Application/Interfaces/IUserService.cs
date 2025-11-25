

using UsersApp.Domain.Entities.Users;

namespace UsersApp.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);

        Task<bool> EmailExistsAsync(string email);
        Task<bool> UsernameExistsAsync(string username);

        Task<User?> GetByEmailAsync(string email);

        Task ImportExternalUsersAsync();
    }
}

