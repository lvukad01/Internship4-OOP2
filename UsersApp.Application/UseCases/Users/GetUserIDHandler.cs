using UsersApp.Application.DTOs.Users;
using UsersApp.Application.UseCases.IUsers;
using UsersApp.Domain.Repositories;

namespace UsersApp.Application.UseCases.Users
{
    public class GetUserByIdHandler : IGetUserById
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<UserResponse> ExecuteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new Exception($"Korisnik s ID {id} ne postoji.");

            var response = new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                AddressStreet = user.AddressStreet,
                AddressCity = user.AddressCity,
                GeoLat = user.GeoLat,
                GeoLng = user.GeoLng,
                Website = user.Website,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                CompanyName = null
            };

            return response;
        }
    }
}
