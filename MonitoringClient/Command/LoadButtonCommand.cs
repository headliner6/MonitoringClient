﻿using MonitoringClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonitoringClient.Command
{
    public class LoadButtonCommand : ICommand
    {
        private LogEntryViewModel _logentriesViewModel;
        public event EventHandler CanExecuteChanged;
                
        public LoadButtonCommand(LogEntryViewModel lvm)
        {
            this._logentriesViewModel = lvm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            _logentriesViewModel.GetAll();
        }
    }
}
