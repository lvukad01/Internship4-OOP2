
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using UsersApp.Domain.Entities.Users;
using UsersApp.Domain.Repositories;
using UsersApp.Infrastructure.Database.Contexts.Users;

namespace UsersApp.Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersDbContext _dbContext;
        private readonly IDbConnection _dbConnection;

        public UserRepository(UsersDbContext dbContext, IDbConnection dbConnection)
        {
            _dbContext = dbContext;
            _dbConnection = dbConnection;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var sql = "SELECT * FROM Users";
            return await _dbConnection.QueryAsync<User>(sql);
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Users WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var sql = "SELECT * FROM Users WHERE Email = @Email";
            return await _dbConnection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            var sql = "SELECT COUNT(1) FROM Users WHERE Email = @Email";
            var count = await _dbConnection.ExecuteScalarAsync<int>(sql, new { Email = email });
            return count > 0;
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            var sql = "SELECT COUNT(1) FROM Users WHERE Username = @Username";
            var count = await _dbConnection.ExecuteScalarAsync<int>(sql, new { Username = username });
            return count > 0;
        }

        public async Task AddAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

    }
}
