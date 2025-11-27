
using UsersApp.Application.DTOs.Users;

namespace UsersApp.Application.UseCases.IUsers
{
    public interface IGetUserById
    {
        Task<UserResponse> ExecuteAsync(int id);
    }
}
