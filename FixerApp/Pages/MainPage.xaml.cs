using FixerApp.ViewModels;

namespace FixerApp.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageVM vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

    }

}
