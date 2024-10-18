using FixerApp.ViewModels;

namespace FixerApp.Pages
{
    public partial class KalenderPage : ContentPage
    {
        public KalenderPage(KalenderPageVM vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

    }

}
