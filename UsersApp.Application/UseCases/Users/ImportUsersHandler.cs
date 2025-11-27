using System.Net.Http;
using System.Text.Json;
using UsersApp.Domain.Entities.Users;
using UsersApp.Domain.Repositories;
using UsersApp.Application.UseCases.IUsers;

namespace UsersApp.Application.UseCases.Users
{
    public class ImportUsersHandler : IImportUsers
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly HttpClient _httpClient;

        private static DateTime? _cacheDate;
        private static List<User>? _cachedUsers;

        public ImportUsersHandler(
            IUserRepository userRepository,
            ICompanyRepository companyRepository,
            HttpClient httpClient)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _httpClient = httpClient;
        }

        public async Task ExecuteAsync()
        {
            if (_cachedUsers != null && _cacheDate?.Date == DateTime.Today)
            {
                return;
            }

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new Exception("Vanjski API nije dostupan.");
            }

            var json = await response.Content.ReadAsStringAsync();
            var externalUsers = JsonSerializer.Deserialize<List<ExternalUserDto>>(json);

            if (externalUsers == null) return;

            foreach (var ext in externalUsers)
            {
                if (await _userRepository.EmailExistsAsync(ext.Email) ||
                    await _userRepository.UsernameExistsAsync(ext.Username))
                    continue; 

                var company = await _companyRepository.GetByNameAsync(ext.CompanyName);
                if (company == null)
                {
                    company = new Domain.Entities.Companies.Company(ext.CompanyName);
                    await _companyRepository.AddAsync(company);
                }

                var user = new User(
                    ext.Name,
                    ext.Username,
                    ext.Email,
                    ext.AddressStreet,
                    ext.AddressCity,
                    ext.GeoLat,
                    ext.GeoLng,
                    ext.Website,
                    Guid.NewGuid().ToString() 
                );

                await _userRepository.AddAsync(user);
            }

            _cachedUsers = externalUsers
                .Select(u => new User(
                    u.Name,
                    u.Username,
                    u.Email,
                    u.AddressStreet,
                    u.AddressCity,
                    u.GeoLat,
                    u.GeoLng,
                    u.Website,
                    Guid.NewGuid().ToString()))
                .ToList();
            _cacheDate = DateTime.Today;
        }
    }

    public class ExternalUserDto
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
