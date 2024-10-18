using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FixerApp.Data;
using FixerApp.Models;
using FixerApp.Pages;

namespace FixerApp.ViewModels
{
    public partial class MainPageVM : ObservableObject
    {
        readonly IDatabase _database;

        public MainPageVM(IDatabase database)
        {
            _database = database;

        }


        #region Commands

        //Go to add a job
        [RelayCommand]
        async Task OnAddFixerJob()
        {
            await Shell.Current.GoToAsync("//addFixerJobPage");
        }


        
        [RelayCommand]
        async Task OnKalenderPage()
        {
            await Shell.Current.GoToAsync("//kalenderPage");
        }



        #endregion

        #region methods

        #endregion
    }
}

