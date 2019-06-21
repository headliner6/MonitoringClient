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
        private DuplicateChecker _duplicateChecker;
        private LogEntryModelRepository _logEntryModelRepository;
        private ObservableCollection<LogEntryModel> _logentries;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand NavigateLogMessageAddView { get; set; }
        public ICommand NavigateLocationView { get; set; }
        public ObservableCollection<LogEntryModel> Logentries
        {
            get { return _logentries; }
            set
            {
                _logentries = value;
                OnPropertyChanged("Logentries");
            }
        }
        public string ConnectionString { get; set; }
        public LoadButtonCommand LoadButtonCommand { get; set; }
        public ConfirmButtonCommand ConfirmButtonCommand { get; set; }
        public FindDuplicatesButtonCommand FindDuplicatesButtonCommand { get; set; }

        public LogEntryViewModel(Action<object> navigateToLogMessageAddView, Action<object> navigateToLocationView)
        {
            NavigateLogMessageAddView = new BaseCommand(StartLogMessageAddView);
            NavigateLocationView = new BaseCommand(StartLocationView);
            LoadButtonCommand = new LoadButtonCommand(this);
            ConfirmButtonCommand = new ConfirmButtonCommand(this);
            FindDuplicatesButtonCommand = new FindDuplicatesButtonCommand(this);
            this.navigateToLogMessageAddView = navigateToLogMessageAddView;
            this.navigateToLocationView = navigateToLocationView;

            Logentries = new ObservableCollection<LogEntryModel>();
            _duplicateChecker = new DuplicateChecker();
            _logEntryModelRepository = new LogEntryModelRepository();

            ConnectionString = _logEntryModelRepository.ConnectionString;
        }
        public void GetAll()
        {
            _logEntryModelRepository.ConnectionString = ConnectionString;
            Logentries = new ObservableCollection<LogEntryModel>(_logEntryModelRepository.GetAll());
        }
        public void ConfirmLogentries(int id)
        {
            _logEntryModelRepository.ConfirmLogentries(id);
        }
        public void CheckForDuplicates()
        {
            this.GetAll();
            Logentries = new ObservableCollection<LogEntryModel>(_duplicateChecker.FindDuplicates(Logentries).Cast<LogEntryModel>());
            if (Logentries.Count == 0)
            {
                MessageBox.Show("Keine Duplikate vorhanden!");
                this.GetAll();
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
    }
}
