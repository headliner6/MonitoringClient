using DuplicateCheckerLib;
using MonitoringClient.Command;
using MonitoringClient.Model;
using MonitoringClient.Repository;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MonitoringClient.ViewModel
{
    public class LogEntryViewModel : INotifyPropertyChanged, IViewModel
    {
        private readonly Action<object> navigateToLogMessageAddView;
        private readonly Action<object> navigateToLocationView;
        private readonly Action<object> navigateToCustomerView;
        private DuplicateChecker _duplicateChecker;
        private LogEntryModelRepository _logEntryModelRepository;
        private ObservableCollection<LogEntries> _logentries;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand NavigateLogMessageAddView { get; set; }
        public ICommand NavigateLocationView { get; set; }
        public ICommand NavigateCustomerView { get; set; }
        public ObservableCollection<LogEntries> Logentries
        {
            get { return _logentries; }
            set
            {
                _logentries = value;
                OnPropertyChanged("Logentries");
            }
        }
        public LoadAllLogEntriesCommand LoadButtonCommand { get; set; }
        public ConfirmButtonCommand ConfirmButtonCommand { get; set; }
        public FindDuplicatesButtonCommand FindDuplicatesButtonCommand { get; set; }

        public LogEntryViewModel(Action<object> navigateToLogMessageAddView, Action<object> navigateToLocationView, Action<object> navigateToCustomerView)
        {
            NavigateLogMessageAddView = new BaseCommand(StartLogMessageAddView);
            NavigateLocationView = new BaseCommand(StartLocationView);
            NavigateCustomerView = new BaseCommand(StartCustomerView);
            LoadButtonCommand = new LoadAllLogEntriesCommand(this);
            ConfirmButtonCommand = new ConfirmButtonCommand(this);
            FindDuplicatesButtonCommand = new FindDuplicatesButtonCommand(this);
            this.navigateToLogMessageAddView = navigateToLogMessageAddView;
            this.navigateToLocationView = navigateToLocationView;
            this.navigateToCustomerView = navigateToCustomerView;

            Logentries = new ObservableCollection<LogEntries>();
            _duplicateChecker = new DuplicateChecker();
            _logEntryModelRepository = new LogEntryModelRepository();
        }
        public void GetAll()
        {

            try
            {
                Logentries = new ObservableCollection<LogEntries>(_logEntryModelRepository.GetAll());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        }
        public void ConfirmLogentries(long id)
        {
            _logEntryModelRepository.ConfirmLogentries(id);
        }
        public void CheckForDuplicates()
        {
            var logentries = new ObservableCollection<LogEntries>(_duplicateChecker.FindDuplicates(Logentries).Cast<LogEntries>());
            if (logentries.Count == 0)
            {
                MessageBox.Show("Keine Duplikate vorhanden!");
            }
            else
            {
                Logentries = logentries;
            }
        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void StartLogMessageAddView(object obj)
        {
            navigateToLogMessageAddView.Invoke("LogMessageAddView");
        }
        private void StartLocationView(object obj)
        {
            navigateToLocationView.Invoke("LocationView");
        }
        private void StartCustomerView(object obj)
        {
            navigateToCustomerView.Invoke("CustomerView");
        }
    }
}