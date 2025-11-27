

using UsersApp.Application.DTOs.Users;

namespace UsersApp.Application.UseCases.IUsers
{
    public interface ICreateUser
    {
        Task<CreateUserResponse> ExecuteAsync(CreateUserRequest request);
    }
}
