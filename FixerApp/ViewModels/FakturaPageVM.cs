using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FixerApp.Data;
using FixerApp.Models;
using System.Threading.Tasks;

namespace FixerApp.ViewModels
{
    public partial class FakturaPageVM : ObservableObject
    {
        readonly IDatabase _database;
        private FixerJob _fixerJob;

        public FakturaPageVM(IDatabase database)
        {
            _database = database;
        }

        #region Properties

        [ObservableProperty]
        string nameOfFixer;

        [ObservableProperty]
        string materials;

        [ObservableProperty]
        decimal materialsPrice;

        [ObservableProperty]
        double hoursWorked;

        [ObservableProperty]
        decimal totalPrice;

        #endregion

        #region Commands

        [RelayCommand]
        async Task SaveFaktura()
        {
            if (_fixerJob == null)
                return;

            var faktura = new Faktura
            {
                NameOfFixer = NameOfFixer,
                Materials = Materials,
                MaterialsPrice = MaterialsPrice,
                HoursWorked = HoursWorked,
                TotalPrice = TotalPrice,
                FixerJobId = _fixerJob.Id // Attach to the FixerJob
            };

            await _database.AddFaktura(faktura);
            await Shell.Current.GoToAsync("//mainPage");
        }

        public async Task Initialize(int fixerJobId)
        {
            // Load the specific FixerJob that this Faktura will be attached to
            _fixerJob = await _database.GetFixerJob(fixerJobId);
        }

        #endregion
    }
}