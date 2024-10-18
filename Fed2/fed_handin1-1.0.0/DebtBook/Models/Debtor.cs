using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace DebtBook.Models
{
	public class Loan
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; } // Maybe not needed
		public double Amount { get; set; }
		public DateTime Date { get; set; }
		public int DebtorId { get; set; } // Foreign key
	}
	public class Debtor
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; } // Maybe not needed
		public string Name { get; set; } // Name of the debtor
		public double Total { get; set; } // Total amount of money owed by the debtor

		[OneToMany(CascadeOperations = CascadeOperation.All)]
		public List<Loan> Loans { get; set; } // List of loans the debtor has taken
	}
}
