﻿using MonitoringClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonitoringClient.Command
{
    public class CreateNewCustomerCommand : ICommand
    {
        private CustomerViewModel _customerViewModel;
        public event EventHandler CanExecuteChanged;

        public CreateNewCustomerCommand(CustomerViewModel cvm)
        {
            this._customerViewModel = cvm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            _customerViewModel.ClearPropertiesAndSelectedItem();
        }
    }
}