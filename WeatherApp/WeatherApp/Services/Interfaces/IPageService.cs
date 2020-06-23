using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WeatherApp.Services.Interfaces
{
    public interface IPageService
    {
        Task PushAsync(Page page);
    }
}
