
using UsersApp.Application.DTOs.Companies;

namespace UsersApp.Application.UseCases.ICompanies
{
    public interface IGetCompanyID
    {
        Task<CompanyResponse> ExecuteAsync(int id, string username, string password);
    }
}
