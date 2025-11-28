using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Json;
using UsersApp.Application.UseCases.IUsers;
using UsersApp.Domain.Entities.Users;
using UsersApp.Domain.Repositories;

namespace UsersApp.Infrastructure.External
{
    public class ExternalUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IUserRepository _userRepository;
        private readonly IMemoryCache _cache; 

        private const string CacheKey = "ExternalUsers";

        public ExternalUserService(HttpClient httpClient, IUserRepository userRepository, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _userRepository = userRepository;
            _cache = cache;
        }

        public async Task<List<User>> ExecuteAsync()
        {
            if (_cache.TryGetValue(CacheKey, out List<User> cachedUsers))
            {
                return cachedUsers;
            }

            var externalUsers = await _httpClient.GetFromJsonAsync<List<ExternalUserDto>>("https://jsonplaceholder.typicode.com/users");

            if (externalUsers == null)
                throw new HttpRequestException("Vanjski API nije dostupan.");

            var users = new List<User>();

            foreach (var eu in externalUsers)
            {
                if (await _userRepository.EmailExistsAsync(eu.Email) || await _userRepository.UsernameExistsAsync(eu.Username))
                    continue;

                var password = Guid.NewGuid().ToString();

                var user = new User(
                    name: eu.Name,
                    username: eu.Username,
                    email: eu.Email,
                    addressStreet: eu.Address.Street,
                    addressCity: eu.Address.City,
                    geoLat: decimal.Parse(eu.Address.Geo.Lat),
                    geoLng: decimal.Parse(eu.Address.Geo.Lng),
                    website: eu.Website,
                    password: password
                );

                users.Add(user);

                await _userRepository.AddAsync(user);
            }

            var timeToExpire = DateTime.Today.AddDays(1) - DateTime.Now;
            _cache.Set(CacheKey, users, timeToExpire);

            return users;
        }
    }

    public class ExternalUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public ExternalAddress Address { get; set; } = null!;
        public string? Website { get; set; }
    }

    public class ExternalAddress
    {
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public ExternalGeo Geo { get; set; } = null!;
    }

    public class ExternalGeo
    {
        public string Lat { get; set; } = null!;
        public string Lng { get; set; } = null!;
    }
}
