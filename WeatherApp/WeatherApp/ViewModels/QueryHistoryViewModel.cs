using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Services;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class QueryHistoryViewModel : INotifyPropertyChanged
    {
        Database Database => DependencyService.Get<Database>();

        private List<HistoryEntry> loadedEntries { get; set; }
        private ObservableCollection<HistoryEntry> _entries { get; set; }
        public ObservableCollection<HistoryEntry> Entries 
        {
            get { return _entries; }
            set { _entries = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public QueryHistoryViewModel()
        {

        }

        public async Task LoadData()
        {
            var entries = await Database.GetItems<HistoryEntry>();

            loadedEntries = entries.Select(entry => new HistoryEntry
            {
                Id = entry.Id,
                CityName = entry.CityName 
            })
                .OrderByDescending(e => e.Id)
                .ToList();

            if (loadedEntries.Count > 5)
            {
                loadedEntries = await RemoveExtraEntries(loadedEntries);
            }

            Entries = new ObservableCollection<HistoryEntry>(loadedEntries);
        }

        private async Task<List<HistoryEntry>> RemoveExtraEntries(List<HistoryEntry> dbEntries)
        {
            var entryToRemove = dbEntries.Last();
            await Database.DeleteItemAsync<HistoryEntry>(entryToRemove.Id);
            dbEntries.Remove(entryToRemove);

            return dbEntries;
        }
    }
}
