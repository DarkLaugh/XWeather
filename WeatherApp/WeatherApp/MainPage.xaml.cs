using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Helpers;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels;
using Xamarin.Forms;

namespace WeatherApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public CurrentWeatherViewModel ViewModel
        {
            get { return BindingContext as CurrentWeatherViewModel; }
            set { BindingContext = value; }
        }

        public MainPage()
        {
            InitializeComponent();

            ViewModel = new CurrentWeatherViewModel();

            MainRelativeLayout.Children.Add(InformationStack,
                Constraint.RelativeToParent(parent => parent.Width / 2 - getWidth(parent, InformationStack) / 2),
                Constraint.RelativeToParent(parent => parent.Height / 2 - getHeight(parent, InformationStack) / 1.7));

            double getWidth(RelativeLayout parent, View view) => view?.Measure(parent.Width, parent.Height).Request.Width ?? -1;
            double getHeight(RelativeLayout parent, View view) => view?.Measure(parent.Width, parent.Height).Request.Height ?? -1;
        }

        private void OnSearchButtonClicked(object sender, EventArgs e)
        {
            var city = CityEntry.Text;
            ViewModel.GetWeatherCommand.Execute(city);
        }

        private void OnHistoryButtonClicked(object sender, EventArgs e)
        {
            ViewModel.GoToHistoryCommand.Execute(null);
        }
    }
}
