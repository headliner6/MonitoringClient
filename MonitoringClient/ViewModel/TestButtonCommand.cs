using MonitoringClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonitoringClient.ViewModel
{
    public class TestButtonCommand : ICommand
    {

        private LogentriesViewModel _logentriesViewModel;
        public event EventHandler CanExecuteChanged;

        public TestButtonCommand(LogentriesViewModel lvm)
        {
            this._logentriesViewModel = lvm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            _logentriesViewModel.Test();
        }
    }
}
