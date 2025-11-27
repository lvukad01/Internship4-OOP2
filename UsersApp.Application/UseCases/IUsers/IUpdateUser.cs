
using UsersApp.Application.DTOs.Users;

namespace UsersApp.Application.UseCases.IUsers
{
    public interface IUpdateUser
    {
        Task<UpdateUserResponse> ExecuteAsync(UpdateUserRequest request, int id);
    }
}
