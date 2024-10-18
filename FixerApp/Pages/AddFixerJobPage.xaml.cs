using FixerApp.ViewModels;

namespace FixerApp.Pages
{
    public partial class AddFixerJobPage : ContentPage
    {
        public AddFixerJobPage(AddFixerJobVM vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

    }

}
