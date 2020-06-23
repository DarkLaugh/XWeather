using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.Services.Interfaces;
using WeatherApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class CurrentWeatherViewModel : INotifyPropertyChanged
    {
        ICurrentWeatherService currentWeatherService => DependencyService.Get<ICurrentWeatherService>();
        IPageService pageService => DependencyService.Get<IPageService>();
        Database Database => DependencyService.Get<Database>();

        private CurrentWeather _currentWeatherInfo { get; set; }
        public CurrentWeather CurrentWeatherInfo
        {
            get { return _currentWeatherInfo; }
            set
            {
                if (_currentWeatherInfo == value)
                {
                    return;
                }

                _currentWeatherInfo = value;
                IconSource = ImageSource.FromResource("WeatherApp.images." + _currentWeatherInfo.Weather[0].Icon + ".png");
                OnPropertyChanged();
            }
        }

        private ImageSource _iconSource { get; set; }
        public ImageSource IconSource
        {
            get { return _iconSource; }
            set 
            {
                if (_iconSource == value)
                {
                    return;
                }

                _iconSource = value; 
                OnPropertyChanged(); 
            }
        }

        private bool _isLoading { get; set; }
        public bool IsLoading
        {
            get { return _isLoading; }
            set 
            {
                if (_isLoading == value)
                {
                    return;
                }

                _isLoading = value;
                OnPropertyChanged(); 
            }
        }

        public ICommand GetWeatherCommand { get; set; }
        public ICommand GoToHistoryCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CurrentWeatherViewModel()
        {
            GetWeatherForCoordinates();
            GetWeatherCommand = new Command<string>(GetWeatherForCity);
            GoToHistoryCommand = new Command(GoToHistory);
        }

        private async void GetWeatherForCity(string city)
        {
            IsLoading = true;
            CurrentWeatherInfo = await currentWeatherService.GetCurrentWeather(city);
            HistoryEntry entry = new HistoryEntry
            {
                CityName = city
            };
            await Database.SaveAsync(entry);
            IsLoading = false;
        }

        private void GoToHistory()
        {
            pageService.PushAsync(new QueryHistory());
        }

        private async void GetWeatherForCoordinates()
        {
            IsLoading = true;
            await Geolocation.GetLocationAsync(new GeolocationRequest { DesiredAccuracy = GeolocationAccuracy.Lowest, Timeout = TimeSpan.FromSeconds(1) });
            CurrentWeatherInfo = await currentWeatherService.GetCurrentWeatherUsingCoordinates();
            IsLoading = false;
        }
    }
}
