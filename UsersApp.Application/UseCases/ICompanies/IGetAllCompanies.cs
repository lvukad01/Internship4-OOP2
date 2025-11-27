
using UsersApp.Application.DTOs.Companies;

namespace UsersApp.Application.UseCases.ICompanies
{
    public interface IGetAllCompanies
    {
        Task<List<CompanyResponse>> ExecuteAsync(string username, string password);
    }
}
