
namespace UsersApp.Application.DTOs.Users
{
    public class CreateUserRequest
    {
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string AddressStreet { get; set; } = null!;
        public string AddressCity { get; set; } = null!;
        public decimal GeoLat { get; set; }
        public decimal GeoLng { get; set; }
        public string? Website { get; set; }
        public string CompanyName { get; set; } = null!;
    }
}
