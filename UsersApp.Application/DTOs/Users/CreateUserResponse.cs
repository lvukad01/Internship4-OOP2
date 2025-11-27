

namespace UsersApp.Application.DTOs.Users
{
    public class CreateUserResponse
    {
        public int UserId { get; set; }
        public string Message { get; set; } = "Korisnik uspješno kreiran.";
    }

}
