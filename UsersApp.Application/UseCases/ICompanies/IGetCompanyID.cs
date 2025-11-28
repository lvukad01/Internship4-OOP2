
using UsersApp.Application.DTOs.Companies;
using UsersApp.Domain.Common.Model;

namespace UsersApp.Application.UseCases.ICompanies
{
    public interface IGetCompanyID
    {
        Task<Result<CompanyResponse>> ExecuteAsync(int id, string username, string password);
    }
}
