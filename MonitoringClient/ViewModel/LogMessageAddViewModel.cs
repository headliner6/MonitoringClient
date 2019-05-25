using MonitoringClient.Command;
using MonitoringClient.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static MonitoringClient.ViewModel.NavigationViewModel;

namespace MonitoringClient.ViewModel
{
    public class LogMessageAddViewModel
    {
        private readonly Action<object> navigate;
        private bool _validationOk;
        private LogentriesModelRepository _logentriesModelRepository;
        public ICommand NavigateAndSave { get; set; }
        public ICommand NavigateBack { get; set; }
        public string POD { set; get; }
        public string Hostname { get; set; }
        public string Severity { get; set; }
        public string Message { get; set; }
        public string ConnectionString { get; set; }

        public LogMessageAddViewModel(Action<object> navigate)
        {
            NavigateBack = new BaseCommand(OnNavigateBack);
            NavigateAndSave = new BaseCommand(OnNavigateAndSave);
            this.navigate = navigate;
            _logentriesModelRepository = new LogentriesModelRepository();
        }
        private void ValidationOfProperties() // TODO: Exception werfen anstelle von nur "MessageBox". Dann muss _validationOk nicht mehr verwendet werden.
        {
            if (string.IsNullOrEmpty(POD))
            {
                MessageBox.Show("POD darf nicht leer sein!");
            }
            else if (string.IsNullOrEmpty(Severity))
            {
                MessageBox.Show("Severity darf nicht leer sein!");
            }
            else if (Regex.IsMatch(Severity, "[^0-9]"))
            {
                MessageBox.Show("Severity darf nur Zahlen enthalten!");
                Severity = Severity.Remove(Severity.Length - 1);
            }
            else if (string.IsNullOrEmpty(Hostname))
            {
                MessageBox.Show("Hostname darf nicht leer sein!");
            }
            else if (string.IsNullOrEmpty(Message))
            {
                MessageBox.Show("Message darf nicht leer sein!");
            }
            else
            {
                _validationOk = true;
            }
        }
        private void OnNavigateAndSave(object obj)
        {
            ValidationOfProperties();
            if (_validationOk == true)
            {
                _logentriesModelRepository.ConnectionString = ConnectionString;
                _logentriesModelRepository.AddMessage(POD, Hostname, Severity, Message);
                navigate.Invoke("LogentriesView");
            }
        }
        private void OnNavigateBack(object obj)
        {
            navigate.Invoke("LogentriesView");
        }
    }
}
