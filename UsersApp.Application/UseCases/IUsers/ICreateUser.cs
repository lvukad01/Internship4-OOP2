

using UsersApp.Application.DTOs.Users;
using UsersApp.Domain.Common.Model;

namespace UsersApp.Application.UseCases.IUsers
{
    public interface ICreateUser
    {
        Task<Result<CreateUserResponse>> ExecuteAsync(CreateUserRequest request);
    }
}
