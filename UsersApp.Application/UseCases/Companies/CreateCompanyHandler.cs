using UsersApp.Application.DTOs.Companies;
using UsersApp.Application.UseCases.ICompanies;
using UsersApp.Domain.Common.Model;
using UsersApp.Domain.Common.Validation;
using UsersApp.Domain.Common.Validation.ValidationItems;
using UsersApp.Domain.Entities.Companies;
using UsersApp.Domain.Repositories;

namespace UsersApp.Application.UseCases.Companies
{
    public class CreateCompanyHandler : ICreateCompany
    {
        private readonly ICompanyRepository _companyRepository;

        public CreateCompanyHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Result<CompanyResponse>> ExecuteAsync(CreateCompanyRequest request)
        {
            var validationResult = new ValidationResult();
            var company = new Company(request.Name);

            var domainValidation = company.Validate();
            foreach (var item in domainValidation.Items)
                validationResult.AddValidationItem(item);

            if (await _companyRepository.NameExistsAsync(request.Name))
            {
                validationResult.AddValidationItem(new ValidationItem
                {
                    Code = "Company3",
                    Message = "Kompanija s tim imenom već postoji.",
                    Severity = ValidationSeverity.Error,
                    Type = ValidationType.BusinessRule
                });
            }

            if (validationResult.HasErrors)
            {
                return new Result<CompanyResponse>(null, validationResult);
            }

            await _companyRepository.AddAsync(company);

            var response = new CompanyResponse
            {
                Id = company.Id,
                Name = company.Name
            };

            return new Result<CompanyResponse>(response, validationResult);
        }

    }
}

