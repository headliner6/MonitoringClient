using MonitoringClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonitoringClient.Command
{
    public class FindDuplicatesButtonCommand : ICommand
    {
        private LogEntryViewModel _logEntryViewModel;
        public event EventHandler CanExecuteChanged;
                
        public FindDuplicatesButtonCommand(LogEntryViewModel levm)
        {
            this._logEntryViewModel = levm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            _logEntryViewModel.CheckForDuplicates();
        }
    }
}
