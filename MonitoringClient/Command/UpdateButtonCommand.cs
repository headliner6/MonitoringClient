using MonitoringClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonitoringClient.Command
{
    public class UpdateButtonCommand : ICommand
    {
        private LocationsViewModel _locationsViewModel;
        public event EventHandler CanExecuteChanged;

        public UpdateButtonCommand(LocationsViewModel lcvm)
        {
            this._locationsViewModel = lcvm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
        }
    }
}
