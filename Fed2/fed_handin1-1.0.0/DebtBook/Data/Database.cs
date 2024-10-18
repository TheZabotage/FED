using DebtBook.Models;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DebtBook.Data
{
    public class Database : IDatabase
    {
        private readonly SQLiteAsyncConnection _connection;

        public Database()
        {
            var dataDir = FileSystem.AppDataDirectory;
            var databasePath = Path.Combine(dataDir, "DebtBook.db");

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
            await _connection.CreateTableAsync<Debtor>();
            await _connection.CreateTableAsync<Loan>();
        }

        #region Debtor

        public async Task<int> AddDebtor(Debtor debtor)
        {
            // Ensure loans are added along with the debtor
            foreach (var loan in debtor.Loans)
            {
                await _connection.InsertAsync(loan);
            }
            return await _connection.InsertAsync(debtor);
        }

        public async Task<int> DeleteDebtor(Debtor debtor)
        {
            // Delete associated loans first
            foreach (var loan in debtor.Loans)
            {
                await _connection.DeleteAsync(loan);
            }
            return await _connection.DeleteAsync(debtor);
        }

        public async Task<int> DeleteDebtorById(int id)
        {
			// Delete associated loans first
			var debtor = await GetDebtor(id);
			foreach (var loan in debtor.Loans)
			{
				await _connection.DeleteAsync(loan);
			}
			return await _connection.DeleteAsync(debtor);
		}

        public async Task<Debtor> GetDebtor(int id)
        {
            // Include loans when retrieving the debtor
            return await _connection.GetWithChildrenAsync<Debtor>(id, recursive: true);
        }

        public async Task<List<Debtor>> GetDebtors()
        {
            // Include loans when retrieving all debtors
            return await _connection.GetAllWithChildrenAsync<Debtor>(recursive: true);
        }

        public async Task<int> UpdateDebtor(Debtor debtor)
        {
            // Update associated loans along with the debtor
            foreach (var loan in debtor.Loans)
            {
                await _connection.UpdateAsync(loan);
            }
            return await _connection.UpdateAsync(debtor);
        }
        
        #endregion

        #region Loan

        public async Task<int> AddLoan(Loan loan)
        {
			return await _connection.InsertAsync(loan);
		}

        public async Task<int> DeleteLoan(Loan loan)
        {
            return await _connection.DeleteAsync(loan);
        }

        public async Task<int> DeleteLoanById(int id)
        {
			return await _connection.DeleteAsync<Loan>(id);
		}

        public async Task<Loan> GetLoan(int id)
        {
			return await _connection.GetAsync<Loan>(id);
		}

        public async Task<List<Loan>> GetLoansFromDebtor(int id)
        {
            return await _connection.Table<Loan>().Where(l => l.DebtorId == id).ToListAsync();
        }

        public async Task<int> UpdateLoan(Loan loan)
        {
			return await _connection.UpdateAsync(loan);
		}
        #endregion
    }
}
