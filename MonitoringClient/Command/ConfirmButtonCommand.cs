using MonitoringClient.Model;
using MonitoringClient.ViewModel;
using System;
using System.Linq;
using System.Windows.Input;

namespace MonitoringClient.Command
{
    public class ConfirmButtonCommand : ICommand
    {

        private LogEntryViewModel _logentriesViewModel;
        public event EventHandler CanExecuteChanged;

        public ConfirmButtonCommand(LogEntryViewModel lvm)
        {
            this._logentriesViewModel = lvm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            if (parameter != null)
            {
                System.Collections.IList sellectedItems = (System.Collections.IList)parameter;
                var sellectedItemscollection = sellectedItems.Cast<LogEntryModel>().ToList();
                foreach (var lm in sellectedItemscollection)
                {
                    _logentriesViewModel.ConfirmLogentries(lm.Id);
                    _logentriesViewModel.LoadLogentries();
                }
            }
        }
    }
}
