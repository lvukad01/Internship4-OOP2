

using UsersApp.Application.Interfaces;

namespace UsersApp.Application.UseCases.Users
{
    public class CreateUserUseCase
    {
        private readonly IUserService _userService;

        public CreateUserUseCase(IUserService userService)
        {
            _userService = userService;
        }
        
    }
}
