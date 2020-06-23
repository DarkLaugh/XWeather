using System;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.Services.Interfaces;
using WeatherApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
    public partial class App : Application
    {
        private Database db;
        public App()
        {
            InitializeComponent();
            db = new Database();
            DependencyService.RegisterSingleton(db);
            DependencyService.Register<IPageService, PageService>();
            DependencyService.Register<ICurrentWeatherService, CurrentWeatherService>();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            db.Initialize(new Type[] { typeof(HistoryEntry) });
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
