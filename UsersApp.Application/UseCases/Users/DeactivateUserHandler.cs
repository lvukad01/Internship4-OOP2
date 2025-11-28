
using UsersApp.Application.UseCases.IUsers;
using UsersApp.Domain.Repositories;

namespace UsersApp.Application.UseCases.Users
{
    public class DeactivateUserHandler : IDeactivateUser
    {
        private readonly IUserRepository _userRepository;

        public DeactivateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public async Task ExecuteAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception($"Korisnik s ID {userId} ne postoji.");

            user.Deactivate();
            await _userRepository.UpdateAsync(user);

        }
    }
}
