using System;
using MonitoringClient.ViewModel;
using System.Windows.Input;

 namespace MonitoringClient.Command
{
    public class GetAllLocationsCommand : ICommand
    {
        private LocationViewModel _locationViewModel;
        public event EventHandler CanExecuteChanged;
        public GetAllLocationsCommand(LocationViewModel lvm)
        {
            this._locationViewModel = lvm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        { 
            _locationViewModel.GetAll();
        }
    }
 }

