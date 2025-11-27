using UsersApp.Application.DTOs.Users;
using UsersApp.Application.Interfaces;
using UsersApp.Application.UseCases.IUsers;
using UsersApp.Domain.Common.Validation;
using UsersApp.Domain.Common.Validation.ValidationItems;
using UsersApp.Domain.Entities.Companies;
using UsersApp.Domain.Entities.Users;
using UsersApp.Domain.Repositories;
using UsersApp.Domain.Services;

namespace UsersApp.Application.UseCases.Users
{
    public class CreateUserHandler : ICreateUser
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;

        public CreateUserHandler(IUserRepository userRepository, ICompanyRepository companyRepository)
        {
            if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
            if (companyRepository == null) throw new ArgumentNullException(nameof(companyRepository));

            _userRepository = userRepository;
            _companyRepository = companyRepository;
        }

        public async Task<CreateUserResponse> ExecuteAsync(CreateUserRequest request)
        {
            var password = Guid.NewGuid().ToString();
            var user = new User(
                request.Name,
                request.Username,
                request.Email,
                request.AddressStreet,
                request.AddressCity,
                request.GeoLat,
                request.GeoLng,
                request.Website,
                password
            );

            ValidationResult validationResult = user.Validate();

            if (await _userRepository.UsernameExistsAsync(user.Username))
                validationResult.AddValidationItem(new ValidationItem
                {
                    Code = "UserUnique1",
                    Message = "Korisničko ime već postoji.",
                    Severity = ValidationSeverity.Error,
                    Type = Domain.Common.Validation.ValidationType.FormalValidation
                });

            if (await _userRepository.EmailExistsAsync(user.Email))
                validationResult.AddValidationItem(new ValidationItem
                {
                    Code = "UserUnique2",
                    Message = "Email već postoji.",
                    Severity = ValidationSeverity.Error,
                    Type = ValidationType.FormalValidation
                });

            if (validationResult.HasErrors)
                throw new ValidationException(validationResult); 

            var allUsers = await _userRepository.GetAllAsync();
            foreach (var existingUser in allUsers)
            {
                var distance = DomainService.CalculateDistance(user, existingUser);
                if (distance > 3) 
                {
                    validationResult.AddValidationItem(ValidationItems.User.UserDistance);
                    throw new ValidationException(validationResult);
                }
            }

            Company? company = null;
            if (!string.IsNullOrWhiteSpace(request.CompanyName))
            {
                if (!await _companyRepository.NameExistsAsync(request.CompanyName))
                {
                    company = new Company(request.CompanyName);
                    var companyValidation = company.Validate();
                    if (companyValidation.HasErrors)
                        throw new ValidationException(companyValidation);

                    await _companyRepository.AddAsync(company);
                }
            }

            await _userRepository.AddAsync(user);

            return new CreateUserResponse
            {
                UserId = user.Id,
                Message = "Korisnik uspješno kreiran."
            };
        }
    }

    public class ValidationException : Exception
    {
        public ValidationResult ValidationResult { get; }
        public ValidationException(ValidationResult validationResult) : base("Validacija neuspješna")
        {
            ValidationResult = validationResult;
        }
    }
}

