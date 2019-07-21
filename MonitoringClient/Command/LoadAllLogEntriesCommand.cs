using MonitoringClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonitoringClient.Command
{
    public class LoadAllLogEntriesCommand : ICommand
    {
        private LogEntryViewModel _logEntryViewModel;
        public event EventHandler CanExecuteChanged;
                
        public LoadAllLogEntriesCommand(LogEntryViewModel levm)
        {
            this._logEntryViewModel = levm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            _logEntryViewModel.GetAll();
        }
    }
}
