
using UsersApp.Application.DTOs.Users;
using UsersApp.Domain.Common.Model;

namespace UsersApp.Application.UseCases.IUsers
{
    public interface IUpdateUser
    {
        Task<Result<UpdateUserResponse>> ExecuteAsync(UpdateUserRequest request, int id);
    }
}
