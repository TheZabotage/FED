using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
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

    
    public partial class KalenderPageVM : ObservableObject
    {
        readonly IDatabase _database;

        public KalenderPageVM(IDatabase database)
        {
        
            _database = database;
            _ = Initialize();


        }

        #region Properties

        [ObservableProperty] 
        ObservableCollection<Kalender> kalenders= new();






        #endregion

        #region Commands

        [RelayCommand]
        async Task OnSpecificDate(int id)
        {
            await Shell.Current.GoToAsync($"//specificDatePage?kalenderId={id}");
        }


        [RelayCommand]
        async Task Cancel()
        {
            await Shell.Current.GoToAsync("//mainPage");
        }
        #endregion

        #region methods
        async Task Initialize()
        {
            var kalenderList = await _database.GetKalenders();
            foreach (var kalender in kalenderList)
            {
                Kalenders.Add(kalender);
            }


        }
        #endregion



    }
}

