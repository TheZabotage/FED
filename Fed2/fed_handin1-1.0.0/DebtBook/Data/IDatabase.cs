using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebtBook.Models;

namespace DebtBook.Data
{
	public interface IDatabase
	{
		Task<int> AddDebtor(Debtor debtor);
		Task<int> DeleteDebtor(Debtor debtor);
		Task<int> DeleteDebtorById(int id);
		Task<Debtor> GetDebtor(int id);
		Task<List<Debtor>> GetDebtors();
		Task<int> UpdateDebtor(Debtor debtor);

		Task<int> AddLoan(Loan loan);
		Task<int> DeleteLoan(Loan loan);
		Task<int> DeleteLoanById(int id);
		Task<Loan> GetLoan(int id);
		Task<List<Loan>> GetLoansFromDebtor(int id);
		Task<int> UpdateLoan(Loan loan);
	}
}
