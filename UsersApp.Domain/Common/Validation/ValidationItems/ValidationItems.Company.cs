

namespace UsersApp.Domain.Common.Validation.ValidationItems
{
    public static partial class ValidationItems
    {
        public static class Company
        {
            public static string CodePrefix = nameof(Company);
            public static readonly ValidationItem NameRequired = new ValidationItem
            {
                Code = $"{CodePrefix}1",
                Message = "Potrebno unijeti naziv kompanije.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };
            public static readonly ValidationItem NameLength = new ValidationItem
            {
                Code = $"{CodePrefix}2",
                Message = "Naziv kompanije ne smije biti duži od 100 znakova.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };
        }
    }
}
