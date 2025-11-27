
using UsersApp.Application.DTOs.Companies;

namespace UsersApp.Application.UseCases.ICompanies
{
    public interface IUpdateCompany
    {
        Task<CompanyResponse> ExecuteAsync(UpdateCompanyRequest request, int id);
    }
}
