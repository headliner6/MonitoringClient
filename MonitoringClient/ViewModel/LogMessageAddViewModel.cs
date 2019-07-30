using MonitoringClient.Command;
using MonitoringClient.RegExp;
using MonitoringClient.Repository;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace MonitoringClient.ViewModel
{
    public class LogMessageAddViewModel : IViewModel, IDataErrorInfo, INotifyPropertyChanged
    {
        private readonly Action<object> navigateToLogEntryView;
        private LogEntryModelRepository _logEntryModelRepository;
        private LogMessageValidation _logMessageValidation;
        private string _pod;
        private string _hostname;
        private int? _severity;
        private string _message;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand NavigateAndSave { get; set; }
        public ICommand NavigateBack { get; set; }
        public string POD
        {
            get { return _pod; }
            set
            {
                _pod = value;
                OnPropertyChanged("POD");
            }

        }
        public string Hostname
        {
            get { return _hostname; }
            set
            {
                _hostname = value;
                OnPropertyChanged("Hostname");
            }

        }
        public int? Severity
        {
            get { return _severity; }
            set
            {
                _severity = value;
                OnPropertyChanged("Severity");
            }

        }
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }

        }
        public string ConnectionString { get; set; }

        public LogMessageAddViewModel(Action<object> navigateToLogEntryView)
        {
            NavigateBack = new BaseCommand(OnNavigateBack);
            NavigateAndSave = new BaseCommand(OnNavigateAndSave);
            this.navigateToLogEntryView = navigateToLogEntryView;
            _logEntryModelRepository = new LogEntryModelRepository();
            _logMessageValidation = new LogMessageValidation();
            ClearAllFields();
        }
       
        private void OnNavigateAndSave(object obj)
        {
            if (IsLogMessageValid == true)
            {
                _logEntryModelRepository.AddMessage(POD, Hostname, Severity, Message);
                navigateToLogEntryView.Invoke("LogEntryView");
            }
            else
            {
                MessageBox.Show("Nicht alle Eingabewerte sind gueltig!");
            }
        }
        private void OnNavigateBack(object obj)
        {
            navigateToLogEntryView.Invoke("LogEntryView");
        }

        private void ClearAllFields()
        {
            POD = "";
            Hostname = "";
            Severity = null;
            Message = "";
        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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

        public bool IsLogMessageValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                {
                    if (GetValidationError(property) != null)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        private readonly string[] ValidatedProperties =
        {
            "POD",
            "Hostname",
            "Severity",
            "Message"
        };

        private string GetValidationError(string propertyName)
        {
            string error = null;

            switch (propertyName)
            {
                case "POD":
                    error = _logMessageValidation.PodValidation(_pod);
                    break;

                case "Hostname":
                    error = _logMessageValidation.HostnameValidation(_hostname);
                    break;

                case "Severity":
                    error = _logMessageValidation.SeverityValidation(_severity.ToString());
                    break;
                case "Message":
                    error = _logMessageValidation.MessageValidation(_message);
                    break;
            }
            return error;
        }
    }
}
