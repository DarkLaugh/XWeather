using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Services.Interfaces;
using Xamarin.Forms;

namespace WeatherApp.Services
{
    public class PageService : IPageService
    {
        private Page MainPage => Application.Current.MainPage;

        public async Task PushAsync(Page page)
        {
            await MainPage.Navigation.PushAsync(page);
        }
    }
}
