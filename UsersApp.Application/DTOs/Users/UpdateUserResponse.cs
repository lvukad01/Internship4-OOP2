

namespace UsersApp.Application.DTOs.Users
{
    public class UpdateUserResponse
    {
        public int UserId { get; set; }
        public string Message { get; set; } = "Korisnik uspješno ažuriran.";
    }
}
