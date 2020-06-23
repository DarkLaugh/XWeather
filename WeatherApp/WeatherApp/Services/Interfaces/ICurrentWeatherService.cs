using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services.Interfaces
{
    public interface ICurrentWeatherService
    {
        Task<CurrentWeather> GetCurrentWeather(string city = "Tokyo");
        Task<CurrentWeather> GetCurrentWeatherUsingCoordinates();
    }
}
