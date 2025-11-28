
using UsersApp.Application.DTOs.Companies;
using UsersApp.Domain.Common.Model;

namespace UsersApp.Application.UseCases.ICompanies
{
    public interface IGetAllCompanies
    {
        Task<Result<List<CompanyResponse>>> ExecuteAsync(string username, string password);
    }
}
