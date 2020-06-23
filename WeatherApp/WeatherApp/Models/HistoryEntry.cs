using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Models
{
    public class HistoryEntry
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string CityName { get; set; }

        public override string ToString()
        {
            return CityName;
        }
    }
}
