

namespace UsersApp.Domain.Common.Validation.ValidationItems
{
    public static partial class ValidationItems
    {

        public static class User
        {
            public static string CodePrefix = nameof(User);

            public static readonly ValidationItem NameRequired = new ValidationItem
            {
                Code = $"{CodePrefix}1",
                Message = "Potrebno unijeti ime.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };
            public static readonly ValidationItem UsernameRequired = new ValidationItem
            {
                Code = $"{CodePrefix}2",
                Message = "Potrebno unijeti korisničko ime.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };
            public static readonly ValidationItem EmailRequired = new ValidationItem
            {
                Code = $"{CodePrefix}3",
                Message = "Potrebno unijeti email.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };
            public static readonly ValidationItem StreetAdressRequired = new ValidationItem
            {
                Code = $"{CodePrefix}4",
                Message = "Potrebno unijeti adresu.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };
            public static readonly ValidationItem CityAdressRequired = new ValidationItem
            {
                Code = $"{CodePrefix}5",
                Message = "Potrebno unijeti grad.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };
            public static readonly ValidationItem LatitudeOutOfRange = new ValidationItem
            {
                Code = $"{CodePrefix}6",
                Message = "Latituda mora biti između -90 and 90.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };
            public static readonly ValidationItem LongitudeOutOfRange = new ValidationItem
            {
                Code = $"{CodePrefix}7",
                Message = "Longituda mora biti između -180 and 180.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };
            public static readonly ValidationItem InvalidWebsiteUrl = new ValidationItem
            {
                Code = $"{CodePrefix}8",
                Message = "Pogrešan URL web stranice.",
                Severity = ValidationSeverity.Warning,
                Type = ValidationType.FormalValidation
            };
            public static readonly ValidationItem NameLength= new ValidationItem
            {
                Code = $"{CodePrefix}9",
                Message = $"Ime ne smije biti duže od {Entities.Users.User.NameMaxLength}.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };
            public static readonly ValidationItem AdressStreetLength = new ValidationItem
            {
                Code = $"{CodePrefix}10",
                Message = $"Adresa ne smije biti duža od {Entities.Users.User.AddressStreetMaxLength}.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };
            public static readonly ValidationItem CityStreetLength = new ValidationItem
            {
                Code = $"{CodePrefix}11",
                Message = $"Ime grada ne smije biti duže od {Entities.Users.User.AddressCityMaxLength}.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };
            public static readonly ValidationItem EmailValid = new ValidationItem
            {
                Code = $"{CodePrefix}12",
                Message = "Email nije točan.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };
            public static readonly ValidationItem UserDistance= new ValidationItem
            {
                Code = $"{CodePrefix}13",
                Message = "Korisnici su previše udaljeni.",
                Severity = ValidationSeverity.Info,
                Type = ValidationType.BusinessRule
            };
            public static readonly ValidationItem PasswordRequired = new ValidationItem
            {
                Code = $"{CodePrefix}14",
                Message = "Potrebno unijeti lozinku.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };
        }

    }
}
