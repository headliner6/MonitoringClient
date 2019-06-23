using MonitoringClient.Command;
using MonitoringClient.DataStructures;
using MonitoringClient.Model;
using MonitoringClient.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MonitoringClient.ViewModel
{
    public class CustomerViewModel : INotifyPropertyChanged, IViewModel
    {
        private readonly Action<object> navigateToLogEntryView;
        private ObservableCollection<CustomerModel> _customers;
        private CustomerRepository _customerRepository;
        private CustomerModel _selectedItem;
        private int _addressnumber;
        private string _phoneNumber;
        private string _email;
        private string _website;
        private string _password;

        public ICommand SaveCustomerCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public string ConnectionString { get; set; }
        public CustomerModel SelectedItem
        {
            get
            {
                FillSelectedItemIntoProperties();
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }
        public int Addressnumber
        {
            get
            { return _addressnumber; }
            set
            {
                _addressnumber = value;
                OnPropertyChanged("Addressnumber");
            }
        }
        public string PhoneNumber
        {
            get
            { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
        public string Email
        {
            get
            { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }
        public string Website
        {
            get
            { return _website; }
            set
            {
                _website = value;
                OnPropertyChanged("Website");
            }
        }
        public string Password
        {
            get
            { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public ObservableCollection<CustomerModel> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                OnPropertyChanged("Customers");
            }
        }
        public ICommand NavigateBack { get; set; }

        public CustomerViewModel(Action<object> navigateToLogEntryView)
        {
            _customerRepository = new CustomerRepository();
            NavigateBack = new BaseCommand(OnNavigateBack);
            SaveCustomerCommand = new BaseCommand(SaveCustomer);
            this.navigateToLogEntryView = navigateToLogEntryView;
        }

        public void GetAll()
        {
            try
            {
                _customerRepository.ConnectionString = ConnectionString;
                Customers = new ObservableCollection<CustomerModel>(_customerRepository.GetAll());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        }

        private void SaveCustomer(object obj)
        {
            try
            {
                //ValidationOfProperties();
                _customerRepository.ConnectionString = ConnectionString;
                if (_customerRepository.GetSingle<int>(_selectedItem.Id) != null)
                {
                    _customerRepository.Update(CreateCustomerToSave());
                    this.GetAll();
                }
                else
                {
                    _customerRepository.Add(CreateCustomerToSave());
                    this.GetAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        }
        private CustomerModel CreateCustomerToSave()
        {
            var customer = new CustomerModel
            {
                Id = _selectedItem.Id,
                Firstname = _selectedItem.Firstname,
                Lastname = _selectedItem.Lastname,
                Addressnumber = Addressnumber,
                CustomerAccountNumber = _selectedItem.CustomerAccountNumber,
                PhoneNumber = PhoneNumber,
                Email = Email,
                Website = Website,
                Password = new CreatePasswordHash().GetMD5Hash(Password)
            };
            return customer;
        }

        private void FillSelectedItemIntoProperties()
        {
            if (_selectedItem != null)
            {
                Addressnumber = _selectedItem.Addressnumber;
                PhoneNumber = _selectedItem.PhoneNumber;
                Email = _selectedItem.Email;
                Website = _selectedItem.Website;
                Password = _selectedItem.Password;
            }
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
