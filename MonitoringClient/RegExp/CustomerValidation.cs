﻿using System;
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
                return null;
            }
            if (!Regex.IsMatch(firstname, @"^[a - z] +[0 - 9_\/\s,.-] +$"))
            {
                return "Firstname darf nicht leer sein!";
            }
            return null;
        }

        public string LastnameValidation(string lastname)
        {
            if (lastname == null)
            {
                return null;
            }
            if (!Regex.IsMatch(lastname, @"^[a - z] +[0 - 9_\/\s,.-] +$")) 
            {
                return "Lastname darf nicht leer sein!";
            }
            return null;
        }

        public string AddressnumberValidation(string addressnumber)
        {
            if (addressnumber == null)
            {
                return null;
            }
            if (!Regex.IsMatch(addressnumber, "(^CU[0-9]{5}$)"))
            {
                return "Addressnumber muss zwingend mit Präfix 'CU' beginnen und anschliessend 5 Ziffern beinhalten.";
            }
            return null;
        }
        public string CustomerAccountNumberValidation(string accountNumber)
        {
            if (accountNumber == null)
            {
                return null;
            }
            if (!Regex.IsMatch(accountNumber, "(^[1-8]{1}$)"))
            {
                return "CustomerAccountNumber muss 1-8 sein!";
            }
            return null;
        }
        public string PhoneNumberValidation(string phoneNumber)
        {
            if (phoneNumber == null)
            {
                return null;
            }
            if (true)
            {
                return "Phone number muss ein gültiges Format haben!! Bsp.: +41 75 409 00 00-56";
            }
            return null;
        }
        public string EmailValidation(string email)
        {
            if (email == null)
            {
                return null;
            }
            if (true)
            {
                return "Email address muss ein gültiges Format haben! Bsp.: test@provider.ch";
            }
            return null;
        }
        public string WebsiteValidation(string website)
        {
            if (website == null)
            {
                return null;
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
                return true;
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
