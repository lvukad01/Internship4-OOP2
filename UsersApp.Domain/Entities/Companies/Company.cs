


using UsersApp.Domain.Common.Validation;
using UsersApp.Domain.Common.Validation.ValidationItems;

namespace UsersApp.Domain.Entities.Companies
{
    public class Company
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public const int NameMaxLength = 150;


        public Company(string name)
        {
            Name = name;
        }

        public ValidationResult Validate()
        {
            var validationResult = new ValidationResult();
            if (string.IsNullOrWhiteSpace(Name))
                validationResult.AddValidationItem(ValidationItems.Company.NameRequired);
            else if (Name.Length > NameMaxLength)
                validationResult.AddValidationItem(ValidationItems.Company.NameLength);
            return validationResult;
        }
    }
}
