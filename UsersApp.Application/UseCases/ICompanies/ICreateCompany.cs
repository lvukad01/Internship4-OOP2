
using UsersApp.Application.DTOs.Companies;
using UsersApp.Domain.Common.Model;

namespace UsersApp.Application.UseCases.ICompanies
{
    public interface ICreateCompany
    {
        Task<Result<CompanyResponse>> ExecuteAsync(CreateCompanyRequest request);
    }
}
