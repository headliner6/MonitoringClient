using MonitoringClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonitoringClient.Command
{
    public class GetSingleButtonCommand : ICommand
    {
        private LocationsViewModel _locationsViewModel;
        public event EventHandler CanExecuteChanged;

        public GetSingleButtonCommand(LocationsViewModel lcvm)
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
