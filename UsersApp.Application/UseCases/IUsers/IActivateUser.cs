
namespace UsersApp.Application.UseCases.IUsers
{
    public interface IActivateUser
    {
        Task ExecuteAsync(int userId);
    }
}
