using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DebtBook.Data;
using DebtBook.Models;
using DebtBook.Pages;

namespace DebtBook.ViewModels
{
	public partial class MainPageVM : ObservableObject
	{
		readonly IDatabase _database;
		
		public MainPageVM(IDatabase database)
		{
			_database = database;
			_ = Initialize();
			WeakReferenceMessenger.Default.Register<NewDebtorAddedMessage>(this, (sender, message) => Debtors.Add(message.Value));

			WeakReferenceMessenger.Default.Register<NewLoanAddedMessage>(this, (sender, message) =>
			{
				var debtor = Debtors.FirstOrDefault(d => d.Id == message.Value.Id);
				if (debtor != null)
				{
					debtor.Total = message.Value.Total;
				}
			});
		}
		
		#region Properties

		[ObservableProperty] 
		ObservableCollection<Debtor> debtors = new();
		#endregion

		//Maybe a command here, that then does some routing stuff to AddDebtorVM.
		//How i dont know.

		#region Commands

		[RelayCommand] 
		async Task OnAddDebtor()
		{
			await Shell.Current.GoToAsync("//addDebtorPage");
		}

		[RelayCommand]
		async Task OnSpecificDebtor(int id)
		{
			await Shell.Current.GoToAsync($"//specificDebtorPage?debtorId={id}");
		}



		#endregion

		#region methods

		//Should update to the updated total value after adding a new value in the SpecificDebtor page
		async Task Initialize()
		{
			var debtorlist = await _database.GetDebtors();
            foreach (var debt in debtorlist)
			{
				Debtors.Add(debt);
			}

		}
		#endregion
	}
}
