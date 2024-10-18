using DebtBook.ViewModels;

namespace DebtBook.Pages;

public partial class AddDebtorPage : ContentPage
{
	public AddDebtorPage(AddDebtorVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}