using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersApp.Application.UseCases.ICompanies;
using UsersApp.Domain.Common.Model;
using UsersApp.Domain.Common.Validation;
using UsersApp.Domain.Repositories;

namespace UsersApp.Application.UseCases.Companies
{
    public class DeleteCompanyHandler : IDeleteCompany
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;

        public DeleteCompanyHandler(ICompanyRepository companyRepository,IUserRepository userRepository )
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<bool>> ExecuteAsync(int id, string username, string password)
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
                return new Result<bool>(false, validationResult);
            }

            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null)
            {
                validationResult.AddValidationItem(new ValidationItem
                {
                    Code = "CompanyNotFound",
                    Message = "Kompanija s tim ID-om ne postoji.",
                    Severity = ValidationSeverity.Error,
                    Type = ValidationType.BusinessRule
                });
                return new Result<bool>(false, validationResult);
            }

            await _companyRepository.DeleteAsync(id);

            return new Result<bool>(true, validationResult);
        }
    }
}
