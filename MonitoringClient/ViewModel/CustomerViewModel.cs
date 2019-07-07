using MonitoringClient.Command;
using MonitoringClient.DataStructures;
using MonitoringClient.Model;
using MonitoringClient.RegExp;
using MonitoringClient.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MonitoringClient.ViewModel
{
    public class CustomerViewModel : INotifyPropertyChanged, IViewModel, IDataErrorInfo
    {
        private readonly Action<object> navigateToLogEntryView;
        private ObservableCollection<CustomerModel> _customers;
        private CustomerRepository _customerRepository;
        private CustomerModel _selectedItem;
        private CustomerValidation _customerValidation;
        private string _firstname;
        private string _lastname;
        private string _addressnumber;
        private int _customerAccountNumber;
        private string _phoneNumber;
        private string _email;
        private string _website;

        public ICommand SaveCustomerCommand { get; set; }
        public ICommand CreateNewCustomer { get; set; }
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

        public string Firstname
        {
            get
            { return _firstname; }
            set
            {
                _firstname = value;
                OnPropertyChanged("Firstname");
            }
        }
        public string Lastname
        {
            get
            { return _lastname; }
            set
            {
                _lastname = value;
                OnPropertyChanged("Lastname");
            }
        }
        public string Addressnumber
        {
            get
            { return _addressnumber; }
            set
            {
                _addressnumber = value;
                OnPropertyChanged("Addressnumber");
            }
        }
        public int CustomerAccountNumber
        {
            get
            { return _customerAccountNumber; }
            set
            {
                _customerAccountNumber = value;
                OnPropertyChanged("CustomerAccountNumber");
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
            CreateNewCustomer = new BaseCommand(ClearPropertiesAndSelectedItem);
            this.navigateToLogEntryView = navigateToLogEntryView;
            _customerValidation = new CustomerValidation();
            ClearPropertiesAndSelectedItem();
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

        private void SaveCustomer(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;
            passwordBox.Clear();

            if (isCustomerValid == false && _customerValidation.PasswordValidation(password))
            {
                MessageBox.Show("Nicht alle Eingabewerte sind gueltig!");
            }
            else
            {
                try
                {
                    _customerRepository.ConnectionString = ConnectionString;
                    if (_selectedItem != null)
                    {
                        _customerRepository.Update(CreateCustomerToSave(password));
                    }
                    else
                    {
                        _customerRepository.Add(CreateCustomerToSave(password));
                    }
                    this.GetAll();
                    this.ClearPropertiesAndSelectedItem();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
                }
            }             
        }
        private CustomerModel CreateCustomerToSave(string password)
        {
            var customer = new CustomerModel();
            if (_selectedItem != null)
            {
                customer.Id = _selectedItem.Id;
            }
            customer.Firstname = Firstname;
            customer.Lastname = Lastname;
            customer.Addressnumber = Addressnumber;
            customer.CustomerAccountNumber = CustomerAccountNumber;
            customer.PhoneNumber = PhoneNumber;
            customer.Email = Email;
            customer.Website = Website;
            customer.Password = new CreatePasswordHash().GetMD5Hash(password);
            return customer;
        }
        private void FillSelectedItemIntoProperties()
        {
            if (_selectedItem != null)
            {
                Firstname = _selectedItem.Firstname;
                Lastname = _selectedItem.Lastname;
                Addressnumber = _selectedItem.Addressnumber;
                CustomerAccountNumber = _selectedItem.CustomerAccountNumber;
                PhoneNumber = _selectedItem.PhoneNumber;
                Email = _selectedItem.Email;
                Website = _selectedItem.Website;
            }
        }
        private void ClearPropertiesAndSelectedItem(object obj)
        {
            SelectedItem = null;
            Firstname = "";
            Lastname = "";
            Addressnumber = "";
            CustomerAccountNumber = 0;
            PhoneNumber = "";
            Email = "";
            Website = "";
        }
        private void ClearPropertiesAndSelectedItem()
        {
            SelectedItem = null;
            Firstname = "";
            Lastname = "";
            Addressnumber = "";
            CustomerAccountNumber = 0;
            PhoneNumber = "";
            Email = "";
            Website = "";
        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void OnNavigateBack(object obj)
        {
            navigateToLogEntryView.Invoke("LogEntryView");
        }


        string IDataErrorInfo.Error
        {
            get
            {
                return null;
            }
        }
        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                return GetValidationError(propertyName);
            }
        }

        public bool isCustomerValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                {
                    if(GetValidationError(property) != null)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        private readonly string[] ValidatedProperties =
        {
            "Firstname",
            "Lastname",
            "Addressnumber",
            "CustomerAccountNumber",
            "PhoneNumber",
            "EmailAddress",
            "Website",
        };

        private string GetValidationError(string propertyName)
        {
            string error = null;

            switch (propertyName)
            {
                case "Firstname":
                    error = _customerValidation.FirstnameValidation(_firstname);
                    break;

                case "Lastname":
                    error = _customerValidation.LastnameValidation(_lastname);
                    break;

                case "Addressnumber":
                    error = _customerValidation.AddressnumberValidation(_addressnumber);
                    break;
                case "CustomerAccountNumber":
                    error = _customerValidation.CustomerAccountNumberValidation(_customerAccountNumber.ToString());
                    break;
                case "PhoneNumber":
                    error = _customerValidation.PhoneNumberValidation(_phoneNumber);
                    break;
                case "Email":
                    error = _customerValidation.EmailValidation(_email);
                    break;
                case "Website":
                    error = _customerValidation.WebsiteValidation(_website);
                    break;
            }
            return error;
        }
    }
}
