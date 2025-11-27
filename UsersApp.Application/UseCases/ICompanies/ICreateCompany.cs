
using UsersApp.Application.DTOs.Companies;

namespace UsersApp.Application.UseCases.ICompanies
{
    public interface ICreateCompany
    {
        Task<CompanyResponse> ExecuteAsync(CreateCompanyRequest request);
    }
}
