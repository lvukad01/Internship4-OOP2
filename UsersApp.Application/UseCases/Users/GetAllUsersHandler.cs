using UsersApp.Application.DTOs.Users;
using UsersApp.Application.UseCases.IUsers;
using UsersApp.Domain.Repositories;
public class GetAllUsersHandler : IGetAllUsers
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<List<UserResponse>> ExecuteAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => new UserResponse
        {
            Id = u.Id,
            Name = u.Name,
            Username = u.Username,
            Email = u.Email,
            AddressStreet = u.AddressStreet,
            AddressCity = u.AddressCity,
            GeoLat = u.GeoLat,
            GeoLng = u.GeoLng,
            Website = u.Website,
            IsActive = u.IsActive,
            CreatedAt = u.CreatedAt,
            UpdatedAt = u.UpdatedAt,
            CompanyName = null 
        }).ToList();
    }
}