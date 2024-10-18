using FixerApp.Models;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FixerApp.Data
{
    public class Database : IDatabase
    {
        private readonly SQLiteAsyncConnection _connection;

        public Database()
        {
            var dataDir = FileSystem.AppDataDirectory;
            var databasePath = Path.Combine(dataDir, "FixerApp.db");

            string _dbEncryptionKey = SecureStorage.GetAsync("dbKey").Result;

            if (string.IsNullOrEmpty(_dbEncryptionKey))
            {
                Guid g = Guid.NewGuid();
                _dbEncryptionKey = g.ToString();
                SecureStorage.SetAsync("dbKey", _dbEncryptionKey);
            }

            var dbOptions = new SQLiteConnectionString(databasePath, true, key: _dbEncryptionKey);
            _connection = new SQLiteAsyncConnection(dbOptions);

            _ = Initialise();
        }

        private async Task Initialise()
        {
            await _connection.CreateTableAsync<FixerJob>();
            await _connection.CreateTableAsync<Kalender>();
            await _connection.CreateTableAsync<Faktura>();
        }

        #region FixerJob

        public async Task<int> AddFixerJob(FixerJob fixerJob)
        {
            
            var existingKalender = await _connection.Table<Kalender>()
                .FirstOrDefaultAsync(k => k.Date == fixerJob.DeliveryDateTime);

            if (existingKalender == null)
            {
                var newKalender = new Kalender
                {
                    Date = fixerJob.DeliveryDateTime,
                    FixerJobs = new List<FixerJob> { fixerJob }
                };

                await _connection.InsertAsync(newKalender);
                fixerJob.KalenderId = newKalender.Id; 
            }
            else
            {
                fixerJob.KalenderId = existingKalender.Id;
            }

            return await _connection.InsertAsync(fixerJob);
        }

        public async Task<int> DeleteFixerJob(FixerJob fixerJob)
        {
            
            return await _connection.DeleteAsync(fixerJob);
        }

        public async Task<FixerJob> GetFixerJob(int id)
        {
            return await _connection.GetWithChildrenAsync<FixerJob>(id, recursive: true);
        }

        public async Task<List<FixerJob>> GetFixerJobs()
        {
            return await _connection.GetAllWithChildrenAsync<FixerJob>(recursive: true);
        }

        public async Task<int> UpdateFixerJob(FixerJob fixerJob)
        {
         
            return await _connection.UpdateAsync(fixerJob);
        }

        #endregion

        //Shit we dont need apperently
        #region Kalender

        public async Task<int> AddKalender(Kalender kalender)
        {
            return await _connection.InsertAsync(kalender);
        }

        public async Task<int> DeleteKalender(Kalender kalender)
        {
            return await _connection.DeleteAsync(kalender);
        }

        public async Task<Kalender> GetKalender(int id)
        {
            return await _connection.GetWithChildrenAsync<Kalender>(id, recursive: true);
        }

        public async Task<List<Kalender>> GetKalenders()
        {
            return await _connection.GetAllWithChildrenAsync<Kalender>(recursive: true);
        }

        public async Task<int> UpdateKalender(Kalender kalender)
        {
            return await _connection.UpdateAsync(kalender);
        }



        #endregion

        #region Faktura




        public async Task<int> AddFaktura(Faktura faktura)
        {
            return await _connection.InsertAsync(faktura);
        }

        public async Task<int> DeleteFaktura(Faktura faktura)
        {
            return await _connection.DeleteAsync(faktura);
        }

        public async Task<Faktura> GetFaktura(int id)
        {
            return await _connection.GetAsync<Faktura>(id);
        }

        public async Task<int> UpdateFaktura(Faktura faktura)
        {
            return await _connection.UpdateAsync(faktura);
        }

        #endregion
    }
}
