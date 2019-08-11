using MonitoringClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonitoringClient.Command
{
    public class GetAllCommand : ICommand
    {
        private IViewModel _viewModel;
        public event EventHandler CanExecuteChanged;

        public GetAllCommand(IViewModel vm)
        {
            this._viewModel = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            _viewModel.GetAll();
        }
    }
}
