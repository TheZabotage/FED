using DebtBook.Pages;

namespace DebtBook
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			
			//MainPage = new AppShell();
			MainPage = new AppShell();
		}

		protected override async void OnStart()
		{
			await Shell.Current.GoToAsync("//mainPage");
			base.OnStart();
		}
	}
}
