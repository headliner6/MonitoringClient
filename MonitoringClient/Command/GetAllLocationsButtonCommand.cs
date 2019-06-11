using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonitoringClient.ViewModel;
using System.Windows.Input;

 namespace MonitoringClient.Command
{
    public class GetAllLocationsButtonCommand : ICommand
    {
        private LocationViewModel _locationViewModel;
        public event EventHandler CanExecuteChanged;
        public GetAllLocationsButtonCommand(LocationViewModel lcvm)
        {
            this._locationViewModel = lcvm;
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

