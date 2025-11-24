

namespace UsersApp.Domain.Entities.Users
{
    internal class User
    {
        public class User
        {
            public int Id { get; private set; }
            public string Name { get; private set; }
            public string Username { get; private set; }
            public string Email { get; private set; }
            public string AddressStreet { get; private set; }
            public string AddressCity { get; private set; }
            public decimal GeoLat { get; private set; }
            public decimal GeoLng { get; private set; }
            public string Website { get; private set; }
            public string Password { get; private set; }
            public bool IsActive { get; private set; } = true; public DateTime CreatedAt { get; private set; }
            public DateTime UpdatedAt { get; private set; }

            public User(string name, string username, string email, string addressStreet, string addressCity, decimal geoLat, decimal geoLng, string website, string password)
            { 
                Name = name; 
                Username = username; 
                Email = email; 
                
                AddressStreet = addressStreet; 
                AddressCity = addressCity; 
                GeoLat = geoLat; 
                GeoLng = geoLng; 
                Website = website; 
                Password = password; 
                CreatedAt = DateTime.Now; 
                UpdatedAt = DateTime.Now;
            }
            public void Activate() 
            { 
                if (!IsActive) 
                {
                    IsActive = true; UpdatedAt = DateTime.Now; 
                } 
            }
            public void Deactivate() 
            { if (IsActive) 
                { 
                    IsActive = false; 
                    UpdatedAt = DateTime.Now; 
                } 
            }

        }
    }
}
