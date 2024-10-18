using System;
using System.Collections.Generic;
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

    public class NewFixerJobAddedMessage : ValueChangedMessage<FixerJob>
    {
        public NewFixerJobAddedMessage(FixerJob value) : base(value) { }
    }
    public partial class AddFixerJobVM : ObservableObject
    {
        readonly IDatabase _database;

        public AddFixerJobVM(IDatabase database)
        {
            _database = database;
        }

        #region Properties

        [ObservableProperty]
        string customer = string.Empty;

        [ObservableProperty]
        string customerAddress = string.Empty;
        
        [ObservableProperty]
        string makeAndModel= string.Empty;

        [ObservableProperty]
        string registerNumber=string.Empty;

        [ObservableProperty]
        string deliveryDateTime = string.Empty;

        [ObservableProperty]
        string jobToDo = string.Empty;

        #endregion

        #region Commands

        [RelayCommand]
        async Task AddFixerJob()
        {
            var fixerJob = new FixerJob
            {
                Customer = Customer,
                CustomerAddress = CustomerAddress,
                MakeAndModel = MakeAndModel,
                RegisterNumber = RegisterNumber,
                DeliveryDateTime = DeliveryDateTime,
                JobToDo = JobToDo
            
            };
            await _database.AddFixerJob(fixerJob);

            //Send the entire object
            WeakReferenceMessenger.Default.Send(new NewFixerJobAddedMessage(fixerJob));
            await Shell.Current.GoToAsync("//mainPage");
        }

        [RelayCommand]
        async Task Cancel()
        {
            await Shell.Current.GoToAsync("//mainPage");
        }
        #endregion
    }
}

