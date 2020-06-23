using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WeatherApp.Models
{
    public class CurrentWeather
    {
        public Weather[] Weather { get; set; }
        public Main Main { get; set; }
        public string Name { get; set; }
    }
}
