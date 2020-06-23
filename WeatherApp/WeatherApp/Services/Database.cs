using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class Database
    {
        static SQLiteAsyncConnection connection;

        public SQLiteAsyncConnection Connection = connection;

        public Database()
        {
            connection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public void Initialize(Type[] types)
        {
            using (var conn = new SQLiteConnection(Constants.DatabasePath, Constants.Flags))
            {
                conn.CreateTables(CreateFlags.None, types);
            }
        }

        public async Task<List<T>> GetItems<T>()
            where T : HistoryEntry, new()
        {
            var items = await connection.Table<T>().ToListAsync();
            return items;
        }

        public async Task<int> DeleteAllItems<T>()
            where T : HistoryEntry, new()
        {
            return await connection.DeleteAllAsync<T>();
        }

        public async Task<int> DeleteItemAsync<T>(object primaryKey)
            where T : HistoryEntry, new()
        {
            return await connection.DeleteAsync<T>(primaryKey);
        }

        public async Task SaveAsync<T>(T item)
            where T : HistoryEntry, new()
        {
            await connection.InsertAsync(item);
        }
    }
}
