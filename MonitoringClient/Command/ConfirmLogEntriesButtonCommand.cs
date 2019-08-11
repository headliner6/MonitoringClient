using MonitoringClient.Model;
using MonitoringClient.ViewModel;
using System;
using System.Collections;
using System.Linq;
using System.Windows.Input;

namespace MonitoringClient.Command
{
    public class ConfirmLogEntriesButtonCommand : ICommand
    {

        private LogEntryViewModel _logEntryViewModel;
        public event EventHandler CanExecuteChanged;

        public ConfirmLogEntriesButtonCommand(LogEntryViewModel levm)
        {
            this._logEntryViewModel = levm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            if (parameter != null)
            {
                var sellectedItems = (IList)parameter;
                var sellectedItemscollection = sellectedItems.Cast<LogEntryModel>().ToList();
                foreach (var logEntry in sellectedItemscollection)
                {
                    _logEntryViewModel.ConfirmLogentries(logEntry.Id);
                    _logEntryViewModel.GetAll();
                }
            }
        }
    }
}
