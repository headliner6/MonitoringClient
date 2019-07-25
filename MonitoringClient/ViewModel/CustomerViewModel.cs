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
using System.Text.RegularExpressions;
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
        private string _selectedCountryCode;
        private CustomerValidation _customerValidation;
        private string _firstname;
        private string _firstnameSearch;
        private string _lastname;
        private string _lastanemSearch;
        private string _addressnumber;
        private int _customerAccountNumber;
        private string _phoneNumber;
        private string _email;
        private string _website;

        public SaveCustomerCommand SaveCustomerCommand { get; set; }
        public CreateNewCustomerCommand CreateNewCustomerCommand { get; set; }
        public LoadAllCustomerCommand LoadAllCustomerCommand { get; set; }
        public SearchCustomerCommand SearchCustomerCommand { get; set; }
        public PhoneNumberDetailsCommand PhoneNumberDetailsCommand { get; set; }
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
        public ObservableCollection<CustomerModel> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                OnPropertyChanged("Customers");
            }
        }
        public List<string> CountryCode { get; set; }
        public ICommand NavigateBack { get; set; }

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
        public string FirstnameSearch
        {
            get
            { return _firstnameSearch; }
            set
            {
                _firstnameSearch = value;
                OnPropertyChanged("FirstnameSearch");
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
        public string LastnameSearch
        {
            get
            { return _lastanemSearch; }
            set
            {
                _lastanemSearch = value;
                OnPropertyChanged("LastnameSearch");
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
        public string SelectedCountryCode
        {
            get
            {
                return _selectedCountryCode;
            }
            set
            {
                _selectedCountryCode = value;
                OnPropertyChanged("SelctedCountryCode");
                OnPropertyChanged("PhoneNumber");
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

        public CustomerViewModel(Action<object> navigateToLogEntryView)
        {
            _customerRepository = new CustomerRepository();
            NavigateBack = new BaseCommand(OnNavigateBack);
            SaveCustomerCommand = new SaveCustomerCommand(this);
            CreateNewCustomerCommand = new CreateNewCustomerCommand(this);
            SearchCustomerCommand = new SearchCustomerCommand(this);
            LoadAllCustomerCommand = new LoadAllCustomerCommand(this);
            PhoneNumberDetailsCommand = new PhoneNumberDetailsCommand(this);
            this.navigateToLogEntryView = navigateToLogEntryView;
            _customerValidation = new CustomerValidation();
            CountryCode = new List<string>();
            InitialiseCountryCodes();
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
        public void SaveCustomer(PasswordBox passwordBox)
        {
            var password = passwordBox.Password;
            passwordBox.Clear();

            if (IsCustomerValid(password) == true)
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
            else
            {
                MessageBox.Show("Nicht alle Eingabewerte sind gueltig!");
            }
        }
        public void ClearPropertiesAndSelectedItem()
        {
            SelectedItem = null;
            SelectedCountryCode = CountryCode.First();
            OnPropertyChanged("SelectedCountryCode");
            Firstname = null;
            Lastname = null;
            Addressnumber = null;
            CustomerAccountNumber = 0;
            PhoneNumber = null;
            Email = null;
            Website = null;
        }
        public void SearchCustomer()
        {
            foreach (CustomerModel customer in Customers)
            {
                if (customer.Firstname.Equals(_firstnameSearch) && customer.Lastname.Equals(_lastanemSearch))
                {
                    try
                    {
                        _customerRepository.ConnectionString = ConnectionString;
                        Customers.Clear();
                        Customers.Add(_customerRepository.GetSingle(customer.Id));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
                    }
                    return;
                }
            }
            MessageBox.Show(@"Der Kunde '" + FirstnameSearch + "' '" + LastnameSearch + "' wurde nicht gefunden!");
        }
        public void PhoneNumberDetails()
        {
            string countryCode = "";
            string areaCode = "";
            string telephoneNumber = "";
            string directDialing = "";

            switch (SelectedCountryCode)
            {
                case "Schweiz":
                    if (_customerValidation.PhoneNumberValidation(PhoneNumber, SelectedCountryCode) == null)
                    {
                        var match = Regex.Match(PhoneNumber, @"^(0041|\+41|0)(\s?\(0\)\s?)?(\s?[1-9]{2}\s?)(\/\s?)?([0-9\s]{1,9})([-][0-9]{2})?$");
                        countryCode = match.Groups[1].ToString();
                        areaCode = match.Groups[3].ToString();
                        telephoneNumber = match.Groups[5].ToString();
                        directDialing = match.Groups[6].ToString();
                        MessageBox.Show("Laendervorwahl: " + countryCode + Environment.NewLine + "Ortsvorwahl: " + areaCode + Environment.NewLine + "Rufnummer: " + telephoneNumber + Environment.NewLine + "Durchwahl: " + directDialing);
                    }
                    else
                    {
                        MessageBox.Show("Phone number hat kein gueltiges Format!");
                    }
                    break;

                case "Deutschland":
                    if (_customerValidation.PhoneNumberValidation(PhoneNumber, SelectedCountryCode) == null)
                    {
                        var match = Regex.Match(PhoneNumber, @"^(0049|\+49)(\s?\(0\)\s?)?(\s?\([0-9]{2}\)\s?|\s?[0-9]{2}\s?)([0-9\s]{1,15})([-][0-9]{2})?$");
                        countryCode = match.Groups[1].ToString();
                        areaCode = match.Groups[3].ToString();
                        telephoneNumber = match.Groups[4].ToString();
                        directDialing = match.Groups[5].ToString();
                        MessageBox.Show("Laendervorwahl: " + countryCode + Environment.NewLine + "Ortsvorwahl: " + areaCode + Environment.NewLine + "Rufnummer: " + telephoneNumber + Environment.NewLine + "Durchwahl: " + directDialing);
                    }
                    else
                    {
                        MessageBox.Show("Phone number hat kein gueltiges Format!");
                    }
                    break;

                case "Liechtenstein":
                    if (_customerValidation.PhoneNumberValidation(PhoneNumber, SelectedCountryCode) == null)
                    {
                        var match = Regex.Match(PhoneNumber, @"^(00423|\+423)([0-9\s]{1,10})([-][0-9]{2})?$");
                        countryCode = match.Groups[1].ToString();
                        telephoneNumber = match.Groups[2].ToString();
                        directDialing = match.Groups[3].ToString();
                        MessageBox.Show("Laendervorwahl: " + countryCode + Environment.NewLine + "Ortsvorwahl: " + areaCode + Environment.NewLine + "Rufnummer: " + telephoneNumber + Environment.NewLine + "Durchwahl: " + directDialing);
                    }
                    else
                    {
                        MessageBox.Show("Phone number hat kein gueltiges Format!");
                    }
                    break;
            }
        }
        private void InitialiseCountryCodes()
        {
            CountryCode.Add("Schweiz");
            CountryCode.Add("Deutschland");
            CountryCode.Add("Liechtenstein");
            SelectedCountryCode = CountryCode.First();
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
            customer.Password = new CreatePasswordHash().GetSaltedHash(password);
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
                if (_customerValidation.PhoneNumberValidation(PhoneNumber, "Schweiz") == null)
                {
                    var index = CountryCode.IndexOf("Schweiz");
                    SelectedCountryCode = CountryCode[index];
                    OnPropertyChanged("SelectedCountryCode");
                }
                if (_customerValidation.PhoneNumberValidation(PhoneNumber, "Deutschland") == null)
                {
                    var index = CountryCode.IndexOf("Deutschland");
                    SelectedCountryCode = CountryCode[index];
                    OnPropertyChanged("SelectedCountryCode");
                }
                if (_customerValidation.PhoneNumberValidation(PhoneNumber, "Liechtenstein") == null)
                {
                    var index = CountryCode.IndexOf("Liechtenstein");
                    SelectedCountryCode = CountryCode[index];
                    OnPropertyChanged("SelectedCountryCode");
                }
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
        private bool IsCustomerValid(string password)
        {
            if (_customerValidation.PasswordValidation(password) == false)
            {
                MessageBox.Show("Passwort muss min. 8 Zeichen gross sein, einen Gross- sowie einen Kleinbuchstaben, ein Sonderzeichen und Zwingend eine Zahl beinhalten!");
                return false;
            }
            foreach (string property in ValidatedProperties)
            {
                if (GetValidationError(property) != null)
                {
                    return false;
                }
            }
            return true;
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
                    error = _customerValidation.PhoneNumberValidation(_phoneNumber, _selectedCountryCode);
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
