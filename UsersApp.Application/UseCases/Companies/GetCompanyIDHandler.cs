using UsersApp.Application.DTOs.Companies;
using UsersApp.Application.UseCases.ICompanies;
using UsersApp.Domain.Common.Model;
using UsersApp.Domain.Common.Validation;
using UsersApp.Domain.Repositories;

namespace UsersApp.Application.UseCases.Companies
{
    public class GetCompanyIDHandler : IGetCompanyID
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;

        public GetCompanyIDHandler(ICompanyRepository companyRepository, IUserRepository userRepository)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<CompanyResponse>> ExecuteAsync(int ID, string username, string password)
        {
            var validationResult = new ValidationResult();

            var user = await _userRepository.GetByEmailAsync(username);
            if (user == null || !user.IsActive || user.Password != password)
            {
                validationResult.AddValidationItem(new ValidationItem
                {
                    Code = "UserAuth1",
                    Message = "Neispravan korisnik ili lozinka.",
                    Severity = ValidationSeverity.Error,
                    Type = ValidationType.BusinessRule
                });

                return new Result<CompanyResponse>(null, validationResult);
            }

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

            var response = new CompanyResponse
            {
                Id = company.Id,
                Name = company.Name
            };

            return new Result<CompanyResponse>(response, validationResult);
        }
    }
}