

namespace UsersApp.Application.UseCases.IUsers
{
    public interface IDeactivateUser
    {
        Task ExecuteAsync(int userId);
    }
}
