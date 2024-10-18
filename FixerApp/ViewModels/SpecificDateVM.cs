using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using FixerApp.Data;
using FixerApp.Models;

namespace FixerApp.ViewModels
{

    public partial class SpecificDateVM : ObservableObject
    {

        readonly IDatabase _database;

        public SpecificDateVM(IDatabase database)
        {
            _database = database;
            _ = Initialize();
        }

        #region Properties




        [ObservableProperty]
        Kalender kalender = new();

        [ObservableProperty]
        int kalenderId;

        [ObservableProperty]
        ObservableCollection<FixerJob> fixerJobs = new();

        [ObservableProperty] private string jobToDo;


        #endregion

        #region Commands


        [RelayCommand]
        async Task Close()
        {
            await Shell.Current.GoToAsync("//mainPage");
        }

        #endregion

        #region methods

        public async Task Initialize()
        {
            while (kalenderId == 0)
            {
                await Task.Delay(100);

            }

            Kalender = await _database.GetKalender(KalenderId);

            foreach (var job in Kalender.FixerJobs)
            {
                fixerJobs.Add(job);
            }
        }

        public async void Dispose()
        {
            FixerJobs = new ObservableCollection<FixerJob>();
        }

        #endregion
    }
}
