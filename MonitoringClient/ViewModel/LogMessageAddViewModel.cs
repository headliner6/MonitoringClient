using MonitoringClient.Command;
using MonitoringClient.Repository;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace MonitoringClient.ViewModel
{
    public class LogMessageAddViewModel : IViewModel
    {
        private readonly Action<object> navigateToLogEntryView;
        private bool _validationOk;
        private LogEntryModelRepository _logentriesModelRepository;
        public BaseCommand NavigateAndSave { get; set; }
        public BaseCommand NavigateBack { get; set; }
        public string POD { set; get; }
        public string Hostname { get; set; }
        public string Severity { get; set; }
        public string Message { get; set; }
        public string ConnectionString { get; set; }

        public LogMessageAddViewModel(Action<object> navigateToLogEntryView)
        {
            NavigateBack = new BaseCommand(OnNavigateBack);
            NavigateAndSave = new BaseCommand(OnNavigateAndSave);
            this.navigateToLogEntryView = navigateToLogEntryView;
            _logentriesModelRepository = new LogEntryModelRepository();
        }
        private void ValidationOfProperties()
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
                navigateToLogEntryView.Invoke("LogEntryView");
            }
        }
        private void OnNavigateBack(object obj)
        {
            navigateToLogEntryView.Invoke("LogEntryView");
        }
    }
}
