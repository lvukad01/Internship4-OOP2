using UsersApp.Application.DTOs.Users;
using UsersApp.Application.UseCases.IUsers;
using UsersApp.Domain.Repositories;

namespace UsersApp.Application.UseCases.Users
{
    public class UpdateUserHandler : IUpdateUser
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<UpdateUserResponse> ExecuteAsync(UpdateUserRequest request, int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new Exception("Korisnik ne postoji.");

            if (await _userRepository.UsernameExistsAsync(request.Username) && request.Username != user.Username)
                throw new Exception("Username već postoji.");

            if (await _userRepository.EmailExistsAsync(request.Email) && request.Email != user.Email)
                throw new Exception("Email već postoji.");


            user.Update(
                request.Name,
                request.Username,
                request.Email,
                request.AddressStreet,
                request.AddressCity,
                request.GeoLat,
                request.GeoLng,
                request.Website
            );

            var validation = user.Validate();
            if (validation.HasErrors)
                throw new Exception("Validation failed.");

            await _userRepository.UpdateAsync(user);

            return new UpdateUserResponse
            {
                UserId = user.Id
            };
        }
    }
}
