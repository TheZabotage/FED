using CommunityToolkit.Maui;
using DebtBook.Data;
using DebtBook.Pages;
using DebtBook.ViewModels;
using Microsoft.Extensions.Logging;

namespace DebtBook
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.UseMauiCommunityToolkit()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});
			builder.Services.AddSingleton<IDatabase, Database>();
			builder.Services.AddSingleton<MainPage>();
			builder.Services.AddTransientWithShellRoute<AddDebtorPage, AddDebtorVM>(nameof(AddDebtorPage));
			builder.Services.AddTransientWithShellRoute<SpecificDebtorPage, SpecificDebtorVM>(nameof(SpecificDebtorPage));
			builder.Services.AddTransient<MainPageVM>();

			//Maybe the mainpage should be a singleton, so that the same instance is used everywhere.
			//I doesnt make sense to reload it every time it is navigated to.


#if DEBUG
			builder.Logging.AddDebug();
#endif

			return builder.Build();
		}
	}
}
