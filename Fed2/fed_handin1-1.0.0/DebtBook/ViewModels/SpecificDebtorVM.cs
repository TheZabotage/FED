using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DebtBook.Data;
using DebtBook.Models;

namespace DebtBook.ViewModels
{
	public class NewLoanAddedMessage : ValueChangedMessage<Debtor>
	{
		public NewLoanAddedMessage(Debtor value) : base(value)
		{
		}
	}

	[QueryProperty(nameof(DebtorId), "debtorId")]
	public partial class SpecificDebtorVM : ObservableObject
	{
    
        readonly IDatabase _database;

        public SpecificDebtorVM(IDatabase database)
		{
            _database = database;
			_ = Initialize();
		}

        #region Properties

		[ObservableProperty]
		Debtor debtor = new();

		[ObservableProperty]
		int debtorId;

		[ObservableProperty]
		ObservableCollection<Loan> loans = new();

		[ObservableProperty]
		int value = 0;
		

		#endregion

		#region Commands

		[RelayCommand]
		async Task AddNewDebt()
		{
			var loan = new Loan
			{
				Amount = Value,
				Date = DateTime.Now,
				DebtorId = DebtorId
			};
			await _database.AddLoan(loan);
			Loans.Add(loan);
			Debtor.Total += Value;
			Debtor.Loans.Add(loan);
			await _database.UpdateDebtor(Debtor);
			Value = 0;
			WeakReferenceMessenger.Default.Send(new NewLoanAddedMessage(Debtor));
		}

		[RelayCommand]
		async Task Close()
		{
			await Shell.Current.GoToAsync("//mainPage");
		}

		#endregion

		#region methods
		public async Task Initialize()
		{
			while (DebtorId == 0)
			{
				await Task.Delay(100);
			}
			Debtor = await _database.GetDebtor(DebtorId);
			var loanList = await _database.GetLoansFromDebtor(DebtorId);

			foreach (var loan in loanList)
			{
				var realloan = await _database.GetLoan(loan.Id);
				Loans.Add(realloan);
			}
		}

		public async void Dispose()
		{
			Loans = new ObservableCollection<Loan>();
		}

		#endregion
	}
}
