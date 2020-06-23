using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QueryHistory : ContentPage
    {
        public QueryHistoryViewModel ViewModel 
        {
            get { return BindingContext as QueryHistoryViewModel; }
            set { BindingContext = value; }
        }
        public QueryHistory()
        {
            InitializeComponent();

            ViewModel = new QueryHistoryViewModel();

            HistoryRelativeLayout.Children.Add(HistoryStack,
                Constraint.RelativeToParent(parent => parent.Width / 2 - getWidth(parent, HistoryStack) / 2),
                Constraint.RelativeToParent(parent => parent.Height / 2 - getHeight(parent, HistoryStack) / 4));

            double getWidth(RelativeLayout parent, View view) => view?.Measure(parent.Width, parent.Height).Request.Width ?? -1;
            double getHeight(RelativeLayout parent, View view) => view?.Measure(parent.Width, parent.Height).Request.Height ?? -1;
        }

        protected async override void OnAppearing()
        {
            await ViewModel.LoadData();
            base.OnAppearing();
        }
    }
}