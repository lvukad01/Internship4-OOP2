using Microsoft.AspNetCore.Mvc;
using UsersApp.Application.DTOs.Users;
using UsersApp.Application.UseCases.IUsers;
using UsersApp.Domain.Common.Validation;
namespace UsersApp.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IGetAllUsers _getAllUsers;
        private readonly IGetUserById _getUserById;
        private readonly ICreateUser _createUser;
        private readonly IUpdateUser _updateUser;
        private readonly IActivateUser _activateUser;
        private readonly IDeactivateUser _deactivateUser;
        private readonly IDeleteUser _deleteUser;
        private readonly IImportUsers _importUsers;

        public UsersController(
            IGetAllUsers getAllUsers,
            IGetUserById getUserById,
            ICreateUser createUser,
            IUpdateUser updateUser,
            IActivateUser activateUser,
            IDeactivateUser deactivateUser,
            IDeleteUser deleteUser,
            IImportUsers importUsers)
        {
            _getAllUsers = getAllUsers;
            _getUserById = getUserById;
            _createUser = createUser;
            _updateUser = updateUser;
            _activateUser = activateUser;
            _deactivateUser = deactivateUser;
            _deleteUser = deleteUser;
            _importUsers = importUsers;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserResponse>>> GetAll()
        {
            var users = await _getAllUsers.ExecuteAsync();
            if (users == null || !users.Any())
                return NotFound("No users found.");
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetById(int id)
        {
            var user = await _getUserById.ExecuteAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserResponse>> Create([FromBody] CreateUserRequest request)
        {
            var result = await _createUser.ExecuteAsync(request);
            if (result.ValidationResult.HasErrors)
                return BadRequest(result.ValidationResult.Items);
            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateUserResponse>> Update(int id, [FromBody] UpdateUserRequest request)
        {
            var result = await _updateUser.ExecuteAsync(request, id);
            if (result.ValidationResult.HasErrors)
                return BadRequest(result.ValidationResult.Items);
            return Ok(result.Value);
        }

        [HttpPut("activate/{id}")]
        public async Task<IActionResult> Activate(int id)
        {
            await _activateUser.ExecuteAsync(id);
            return NoContent();
        }

        [HttpPut("deactivate/{id}")]
        public async Task<IActionResult> Deactivate(int id)
        {
            await _deactivateUser.ExecuteAsync(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _deleteUser.ExecuteAsync(id);
            return NoContent();
        }

        [HttpPost("import-external")]
        public async Task<IActionResult> ImportExternal()
        {
            await _importUsers.ExecuteAsync();
            return Ok(new { Message = "Korisnici iz vanjskog API-ja su importirani." });
        }
    }
}
