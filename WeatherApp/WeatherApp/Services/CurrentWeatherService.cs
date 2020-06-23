using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;
using Xamarin.Essentials;

namespace WeatherApp.Services
{
    public class CurrentWeatherService : ICurrentWeatherService
    {
        private readonly HttpClient client;
        private static readonly string apiKey = "74dc22a7945156fc9bd811ab463e4dcc";

        public CurrentWeatherService()
        {
            client = new HttpClient();
        }

        public async Task<CurrentWeather> GetCurrentWeather(string city = "Tokyo")
        {
            var requestURL = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
            var response = await client.GetStringAsync(requestURL);
            var currentWeather = JsonConvert.DeserializeObject<CurrentWeather>(response);

            return currentWeather;
        }

        public async Task<CurrentWeather> GetCurrentWeatherUsingCoordinates()
        {
            var location = await CheckIfDeviceLocationIsAvailable();

            if (location == null)
            {
                var defaultWeather = await GetCurrentWeather();
                return defaultWeather;
            }

            var currentWeather = await GetCurrentWeatherForDeviceLocation(location);
            return currentWeather;
        }

        private async Task<Location> GetDeviceLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Low, TimeSpan.FromSeconds(3));
            var location = await Geolocation.GetLocationAsync(request);

            return location;
        }

        private async Task<Location> GetDeviceLastKnownLocation()
        {
            var locaiton = await Geolocation.GetLastKnownLocationAsync();

            return locaiton;
        }

        private async Task<CurrentWeather> GetCurrentWeatherForDeviceLocation(Location location)
        {
            var requestURL = $"https://api.openweathermap.org/data/2.5/weather?lat={location.Latitude}&lon={location.Longitude}&appid={apiKey}&units=metric";
            var response = await client.GetStringAsync(requestURL);
            var currentWeather = JsonConvert.DeserializeObject<CurrentWeather>(response);

            return currentWeather;
        }

        private async Task<Location> CheckIfDeviceLocationIsAvailable()
        {
            var currLocation = await GetDeviceLocation();

            if (currLocation == null)
            {
                var cachedLocation = await GetDeviceLastKnownLocation();

                return cachedLocation == null ?  null : cachedLocation;
            }

            return currLocation;
        }
    }
}
