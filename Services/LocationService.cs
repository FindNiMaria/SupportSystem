using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpdeskSystem.Services
{
    public class LocationService
    {
        private readonly Dictionary<string, List<string>> _countryCities = new Dictionary<string, List<string>>
        {
            { "Brasil", new List<string> { "São Paulo", "Rio de Janeiro", "Belo Horizonte" } },
            { "Estados Unidos", new List<string> { "Nova York", "Los Angeles", "Chicago" } },
            { "Itália", new List<string> { "Roma", "Milão", "Veneza" } }
        };

        public Task<List<string>> GetCountriesAsync()
        {
            var countries = _countryCities.Keys.ToList();
            return Task.FromResult(countries);
        }

        public Task<List<string>> GetCitiesByCountryAsync(string country)
        {
            if (_countryCities.TryGetValue(country, out var cities))
            {
                return Task.FromResult(cities);
            }
            return Task.FromResult(new List<string>());
        }
    }
}
