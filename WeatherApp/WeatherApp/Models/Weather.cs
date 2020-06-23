using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Helpers;
using Xamarin.Forms;

namespace WeatherApp.Models
{
    public class Weather
    {
        private string _description { get; set; }
        public string Description 
        {
            get { return _description; }
            set { _description = CapitalizeFirstLetter.Capitalize(value); }
        }
        public string Icon { get; set; }
    }
}
