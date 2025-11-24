using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using UsersApp.Domain.Common.Model;
using UsersApp.Domain.Common.Validation;
using UsersApp.Domain.Common.Validation.ValidationItems;
using ValidationResult = UsersApp.Domain.Common.Validation.ValidationResult;

namespace UsersApp.Domain.Entities.Users
{
    
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public const int NameMaxLength = 100;
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string AddressStreet { get; private set; }
        public const int AddressStreetMaxLength = 150;
        public string AddressCity { get; private set; }
        public const int AddressCityMaxLength = 100;
        public decimal GeoLat { get; private set; }
        public decimal GeoLng { get; private set; }
        public string Website { get; private set; }
        public const int WebsiteMaxLength = 100;
        public string Password { get; private set; }
        public bool IsActive { get; private set; } = true; public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public User(string name, string username, string email, string addressStreet, string addressCity, decimal geoLat, decimal geoLng, string website, string password)
        { 
            Name = name; 
            Username = username; 
            Email = email; 
                
            AddressStreet = addressStreet; 
            AddressCity = addressCity; 
            GeoLat = geoLat; 
            GeoLng = geoLng; 
            Website = website; 
            Password = password; 
            CreatedAt = DateTime.Now; 
            UpdatedAt = DateTime.Now;
        }
        public void Activate() 
        { 
            if (!IsActive) 
            {
                IsActive = true; UpdatedAt = DateTime.Now; 
            } 
        }
        public void Deactivate() 
        { if (IsActive) 
            { 
                IsActive = false; 
                UpdatedAt = DateTime.Now; 
            } 
        }
        public ValidationResult Validate()
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(Name))
                result.AddValidationItem(ValidationItems.User.NameRequired);

            if (Name.Length > NameMaxLength)
                result.AddValidationItem(ValidationItems.User.NameLength);

            if (string.IsNullOrWhiteSpace(Username))
                result.AddValidationItem(ValidationItems.User.UsernameRequired);

            if (string.IsNullOrWhiteSpace(Email))
                result.AddValidationItem(ValidationItems.User.EmailRequired);
            else if (!Email.Contains("@"))
                result.AddValidationItem(ValidationItems.User.EmailValid);

            if (string.IsNullOrWhiteSpace(AddressStreet))
                result.AddValidationItem(ValidationItems.User.StreetAdressRequired);
            else if (AddressStreet.Length > AddressStreetMaxLength)
                result.AddValidationItem(ValidationItems.User.AdressStreetLength);

            if (string.IsNullOrWhiteSpace(AddressCity))
                result.AddValidationItem(ValidationItems.User.CityAdressRequired);
            else if (AddressCity.Length > AddressCityMaxLength)
                result.AddValidationItem(ValidationItems.User.CityStreetLength);

            if (GeoLat is < -90 or > 90)
                result.AddValidationItem(ValidationItems.User.LatitudeOutOfRange);

            if (GeoLng is < -180 or > 180)
                result.AddValidationItem(ValidationItems.User.LongitudeOutOfRange);

            if (!string.IsNullOrWhiteSpace(Website)
                && !Uri.IsWellFormedUriString(Website, UriKind.Absolute))
                result.AddValidationItem(ValidationItems.User.InvalidWebsiteUrl);

            return result;
        }
    }
    
}
