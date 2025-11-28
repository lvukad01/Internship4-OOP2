using UsersApp.Application.DTOs.Companies;
using UsersApp.Application.UseCases.ICompanies;
using UsersApp.Domain.Common.Model;
using UsersApp.Domain.Common.Validation;
using UsersApp.Domain.Repositories;

namespace UsersApp.Application.UseCases.Companies
{
    public class GetAllCompanies : IGetAllCompanies
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;

        public GetAllCompanies(ICompanyRepository companyRepository, IUserRepository userRepository)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<List<CompanyResponse>>> ExecuteAsync(string username, string password)
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

                return new Result<List<CompanyResponse>>(new List<CompanyResponse>(), validationResult);
            }

            var companies = await _companyRepository.GetAllAsync();

            var response = companies.Select(c => new CompanyResponse
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return new Result<List<CompanyResponse>>(response, validationResult);
        }
    }
}
