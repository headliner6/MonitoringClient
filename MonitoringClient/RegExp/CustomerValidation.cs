using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace MonitoringClient.RegExp
{
    public class CustomerValidation
    {
        public string FirstnameValidation(string firstname)
        {
            if (firstname == null)
            {
                return "Firstname darf nicht leer sein!";
            }
            if (!Regex.IsMatch(firstname, @"^[a-zA-Z]+$"))
            {
                return "Firstname darf nicht leer sein!";
            }
            return null;
        }

        public string LastnameValidation(string lastname)
        {
            if (lastname == null)
            {
                return "Lastname darf nicht leer sein!";
            }
            if (!Regex.IsMatch(lastname, @"^[a-zA-Z]+$")) 
            {
                return "Lastname darf nicht leer sein!";
            }
            return null;
        }

        public string AddressnumberValidation(string addressnumber)
        {
            if (addressnumber == null)
            {
                return "Addressnumber muss zwingend mit Präfix 'CU' beginnen und anschliessend 5 Ziffern beinhalten.";
            }
            if (!Regex.IsMatch(addressnumber, @"(^CU[0-9]{5}$)"))
            {
                return "Addressnumber muss zwingend mit Präfix 'CU' beginnen und anschliessend 5 Ziffern beinhalten.";
            }
            return null;
        }
        public string CustomerAccountNumberValidation(string accountNumber)
        {
            if (accountNumber == null)
            {
                return "CustomerAccountNumber muss 1-8 sein!";
            }
            if (!Regex.IsMatch(accountNumber, @"^[1-8]{1}$"))
            {
                return "CustomerAccountNumber muss 1-8 sein!";
            }
            return null;
        }
        public string PhoneNumberValidation(string phoneNumber, string SelectedCountryCode)
        {
            switch (SelectedCountryCode)
            {
                case "Schweiz":
                    if (phoneNumber == null || !Regex.IsMatch(phoneNumber, @"^(0|0041|\+41|[0-9]{3})(\s?(\(0\))?\s?|\s?(\/)?\s?)?([1-9\s]|[0-9\s]{1,12})([-][0-9]{2})?$"))
                    {
                        return "Phone number muss ein gültiges Format haben!! Bsp.: +41 75 409 00 00-56";
                    }
                    break;

                case "Deutschland":
                    if (phoneNumber == null || !Regex.IsMatch(phoneNumber, @"^(0049|\+49)(\s?(\(0\))?\s?|\s?(\/)?\s?)?([1-9\s]|[0-9\s]{1,12})([-][0-9]{2})?$"))
                    {
                        return "Phone number muss ein gültiges Format haben!! Bsp.: +49 75 409 00 00-56";
                    }
                    break;

                case "Liechtenstein":
                    if (phoneNumber == null || !Regex.IsMatch(phoneNumber, @"^(00423|\+423)(\s?(\(0\))?\s?|\s?(\/)?\s?)?([1-9\s]|[0-9\s]{1,12})([-][0-9]{2})?$"))
                    {
                        return "Phone number muss ein gültiges Format haben!! Bsp.: +423 75 409 00 00-56";
                    }
                    break;
            }
            return null;
        }
        public string EmailValidation(string email)
        {
            if (email == null)
            {
                return "Email address muss ein gültiges Format haben! Bsp.: test@provider.ch";
            }
            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$"))
            {
                return "Email address muss ein gültiges Format haben! Bsp.: test@provider.ch";
            }
            return null;
        }
        public string WebsiteValidation(string website)
        {
            if (website == null)
            {
                return "Website muss ein gültiges Format haben!Bsp.: www.google.ch";
            }
            if (!Regex.IsMatch(website, @"^(https?:\/\/)?(www\.)?([a-zA-Z0-9]+(-?[a-zA-Z0-9])*\.)+[\w]{2,}(\/\S*)?$"))
            {
                return "Website muss ein gültiges Format haben! Bsp.: www.google.ch";
            }
            return null;
        }
        public bool PasswordValidation(string password)
        {
            if (password == null)
            {
                return false;
            }
            if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})"))
            {
                MessageBox.Show("Passwort muss min. 8 Zeichen gross sein, einen Gross- sowie einen Kleinbuchstaben und Zwingend eine Zahl beinhalten!");
                return false;
            }
            return true;
        }


    }
}
