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
	public class NewDebtorAddedMessage : ValueChangedMessage<Debtor>
	{
		public NewDebtorAddedMessage(Debtor value) : base(value)
		{
		}
	}

	public partial class AddDebtorVM : ObservableObject
	{
		readonly IDatabase _database;

		public AddDebtorVM(IDatabase database)
		{
			_database = database;
		}

        #region Properties

        [ObservableProperty]
		string name = string.Empty;

		[ObservableProperty]
		double initialValue = 0;
		#endregion

		#region Commands

		[RelayCommand]
		async Task AddDebtor()
		{
			var debtor = new Debtor
			{
				Name = Name,
				Total = InitialValue,
				Loans = new List<Loan>()
			};
			await _database.AddDebtor(debtor);
			var loan = new Loan
			{
				Amount = InitialValue,
				Date = DateTime.Now,
				DebtorId = debtor.Id
			};
			await _database.AddLoan(loan);
			debtor.Loans.Add(loan);
			//Send the entire debtor object to the
			WeakReferenceMessenger.Default.Send(new NewDebtorAddedMessage(debtor));
			await Shell.Current.GoToAsync("//mainPage");
		}

		[RelayCommand]
		async Task Cancel()
		{
			await Shell.Current.GoToAsync("//mainPage");
		}
		#endregion
	}
}
