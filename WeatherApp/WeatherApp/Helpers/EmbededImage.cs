using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Helpers
{
    [ContentProperty ("Source")]
    public class EmbededImage : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (String.IsNullOrEmpty(Source))
            {
                return null;
            }

            var imageSource = ImageSource.FromResource(Source, typeof(EmbededImage).GetTypeInfo().Assembly);
            return imageSource;
        }
    }
}
