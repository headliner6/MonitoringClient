using MonitoringClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonitoringClient.Command
{
    public class ExportDataCommand : ICommand
    {
        private IViewModel _viewModel;
        public event EventHandler CanExecuteChanged;

        public ExportDataCommand(IViewModel cvm)
        {
            this._viewModel = cvm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Export":
                    _viewModel.Export();
                    break;
            }
            switch (parameter.ToString())
            {
                case "ChooseExportPath":
                    _viewModel.ChooseExportPath();
                    break;
            }
            switch (parameter.ToString())
            {
                case "ChooseExporterDllPath":
                    _viewModel.ChooseExporterDllPath();
                    break;
            }
        }
    }
}
