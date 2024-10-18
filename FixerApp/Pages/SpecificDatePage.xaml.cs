using FixerApp.ViewModels;

namespace FixerApp.Pages
{

    public partial class SpecificDatePage : ContentPage
    {
        public SpecificDatePage(SpecificDateVM vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = ((SpecificDateVM)BindingContext).Initialize();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((SpecificDateVM)BindingContext).Dispose();
        }
    }
}


