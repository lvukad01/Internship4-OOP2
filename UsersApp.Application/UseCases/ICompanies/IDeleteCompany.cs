using UsersApp.Domain.Common.Model;
using UsersApp.Domain.Repositories;

namespace UsersApp.Application.UseCases.ICompanies
{
    public interface IDeleteCompany
    {
        Task<Result<bool>> ExecuteAsync(int id, string username, string password);
    }
}
