using UsersApp.Domain.Common.Validation;
using UsersApp.Application.DTOs.Companies;
using UsersApp.Application.UseCases.ICompanies;
using UsersApp.Domain.Common.Model;
using UsersApp.Domain.Repositories;
using UsersApp.Domain.Entities.Companies;

namespace UsersApp.Application.UseCases.Companies
{
    public class UpdateCompanyHandler : IUpdateCompany
    {
        private readonly ICompanyRepository _companyRepository;

        public UpdateCompanyHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Result<CompanyResponse>> ExecuteAsync(UpdateCompanyRequest request, int ID)
        {
            var validationResult = new ValidationResult();
            var company = await _companyRepository.GetByIdAsync(ID);
            if (company == null)
            {
                validationResult.AddValidationItem(new ValidationItem
                {
                    Code = "CompanyNotFound",
                    Message = "Kompanija s tim ID-om ne postoji.",
                    Severity = ValidationSeverity.Error,
                    Type = ValidationType.BusinessRule
                });
                return new Result<CompanyResponse>(null, validationResult);
            }
            var tempCompany = new Company(request.Name);
            var domainValidation = tempCompany.Validate();
            foreach (var item in domainValidation.Items)
                validationResult.AddValidationItem(item);

            // 3️⃣ Provjera postoji li već kompanija s tim imenom
            if (await _companyRepository.NameExistsAsync(request.Name) && request.Name != company.Name)
            {
                validationResult.AddValidationItem(new ValidationItem
                {
                    Code = "CompanyNameExists",
                    Message = "Kompanija s tim imenom već postoji.",
                    Severity = ValidationSeverity.Error,
                    Type = ValidationType.BusinessRule
                });
            }

            if (validationResult.HasErrors)
            {
                return new Result<CompanyResponse>(null, validationResult);
            }

            company = new Company(request.Name); 
            await _companyRepository.UpdateAsync(company);

            var response = new CompanyResponse
            {
                Id =company.Id,
                Name =company.Name
            };

            return new Result<CompanyResponse>(response, validationResult);
        }
    }
}