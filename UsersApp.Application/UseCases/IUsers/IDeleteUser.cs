
namespace UsersApp.Application.UseCases.IUsers
{
    public interface IDeleteUser
    {
        Task ExecuteAsync(int userId);
    }
}
