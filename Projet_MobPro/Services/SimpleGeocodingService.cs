using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Projet_MobPro.Services
{
    public class SimpleGeocodingService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<(double Lat, double Lon)> GeocodeAddressAsync(string address)
        {
            try
            {
                var encodedAddress = Uri.EscapeDataString(address);
                var url = $"https://nominatim.openstreetmap.org/search?q={encodedAddress}&format=json&limit=1";

                var response = await _httpClient.GetStringAsync(url);
                var results = JArray.Parse(response);

                if (results.Count > 0)
                {
                    var result = results[0];
                    return (result["lat"].Value<double>(), result["lon"].Value<double>());
                }

                Debug.WriteLine($"Adresse non trouvée : {address}");
                return (0, 0);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors du géocodage de l'adresse {address}: {ex.Message}");
                return (0, 0);
            }
        }

        public double CalculateDistance((double Lat, double Lon) point1, (double Lat, double Lon) point2)
        {
            var d1 = point1.Lat * (Math.PI / 180.0);
            var num1 = point1.Lon * (Math.PI / 180.0);
            var d2 = point2.Lat * (Math.PI / 180.0);
            var num2 = point2.Lon * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }

        public async Task<double> GetDistanceBetweenJobAndProfile(string jobAddress, string profileAddress)
        {
            try
            {
                var jobLocation = await GeocodeAddressAsync(jobAddress);
                var profileLocation = await GeocodeAddressAsync(profileAddress);

                if (jobLocation == (0, 0) || profileLocation == (0, 0))
                {
                    Debug.WriteLine($"Géocodage échoué - Job: {jobAddress}, Profile: {profileAddress}");
                    return -1;
                }

                return CalculateDistance(jobLocation, profileLocation);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors du calcul de la distance : {ex.Message}");
                return -1;
            }
        }
    }
}