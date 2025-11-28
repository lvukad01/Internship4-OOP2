
using UsersApp.Application.DTOs.Companies;
using UsersApp.Domain.Common.Model;

namespace UsersApp.Application.UseCases.ICompanies
{
    public interface IUpdateCompany
    {
        Task<Result<CompanyResponse>> ExecuteAsync(UpdateCompanyRequest request, int id);
    }
}
