
using UsersApp.Domain.Entities.Users;

namespace UsersApp.Domain.Services
{
    public static class DomainService
    {
        public static double CalculateDistance(User user1, User user2)
        {
            var lat1 = (double)user1.GeoLat;
            var lon1 = (double)user1.GeoLng;
            var lat2 = (double)user2.GeoLat;
            var lon2 = (double)user2.GeoLng;

            var R = 6371;
            var dLat = DegreesToRadians(lat2 - lat1);
            var dLon = DegreesToRadians(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        private static double DegreesToRadians(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }
}
