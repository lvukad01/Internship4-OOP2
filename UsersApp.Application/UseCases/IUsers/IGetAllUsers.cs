
using UsersApp.Application.DTOs.Users;

namespace UsersApp.Application.UseCases.IUsers
{
    public interface IGetAllUsers
    {
        Task<List<UserResponse>> ExecuteAsync();
    }
}
