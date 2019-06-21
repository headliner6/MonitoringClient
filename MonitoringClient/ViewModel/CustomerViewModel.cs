using MonitoringClient.Command;
using MonitoringClient.Model;
using MonitoringClient.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonitoringClient.ViewModel
{
    public class CustomerViewModel : INotifyPropertyChanged, IViewModel
    {
        private readonly Action<object> navigateToLogEntryView;
        private ObservableCollection<CustomerModel> _customers;
        private CustomerRepository _customerRepository;

        public event PropertyChangedEventHandler PropertyChanged;
        public string ConnectionString { get; set; }
        public ObservableCollection<CustomerModel> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                OnPropertyChanged("Customers");
            }
        }
        public GetAllCustomersCommand GetAllCustomersCommand { get; set; }
        public ICommand NavigateBack { get; set; }

        public CustomerViewModel(Action<object> navigateToLogEntryView)
        {
            _customerRepository = new CustomerRepository();
            NavigateBack = new BaseCommand(OnNavigateBack);
            this.navigateToLogEntryView = navigateToLogEntryView;

            GetAllCustomersCommand = new GetAllCustomersCommand(this);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void OnNavigateBack(object obj)
        {
            navigateToLogEntryView.Invoke("LogEntryView");
        }
    }
}
