using DebtBook.ViewModels;

namespace DebtBook.Pages;

public partial class SpecificDebtorPage : ContentPage
{
	public SpecificDebtorPage(SpecificDebtorVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_ = ((SpecificDebtorVM)BindingContext).Initialize();
	}

	protected override void OnDisappearing()
	{
		base.OnDisappearing();
		((SpecificDebtorVM)BindingContext).Dispose();
	}
}