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

namespace MonitoringClient.ViewModel
{
    public class LogEntryViewModel : INotifyPropertyChanged, IViewModel
    {
        private readonly Action<object> navigateToLogMessageAddView;
        private DuplicateChecker _duplicateChecker;
        private LogEntryModelRepository _logentriesModelRepository;
        private ObservableCollection<LogEntryModel> _logentries;

        public event PropertyChangedEventHandler PropertyChanged;

        public BaseCommand NavigateLogMessageAddView { get; set; }
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

        public LogEntryViewModel(Action<object> navigateToLogMessageAddView)
        {
            NavigateLogMessageAddView = new BaseCommand(StartLogMessageAddView);
            this.navigateToLogMessageAddView = navigateToLogMessageAddView;

            _logentriesModelRepository = new LogEntryModelRepository();

            ConnectionString = _logentriesModelRepository.ConnectionString;
            LoadButtonCommand = new LoadButtonCommand(this);
            ConfirmButtonCommand = new ConfirmButtonCommand(this);
            Logentries = new ObservableCollection<LogEntryModel>();
            _duplicateChecker = new DuplicateChecker();
            FindDuplicatesButtonCommand = new FindDuplicatesButtonCommand(this);
        }
        public void LoadLogentries()
        {
            _logentriesModelRepository.ConnectionString = ConnectionString;
            Logentries = _logentriesModelRepository.LoadLogentries();
            var repo = new LocationModelRepository();
            repo.ConnectionString = ConnectionString;
            var l = repo.GetAll();
        }
        public void ConfirmLogentries(int id)
        {
            _logentriesModelRepository.ConfirmLogentries(id);
        }
        public void CheckForDuplicates()
        {
            this.LoadLogentries();
            Logentries = new ObservableCollection<LogEntryModel>(_duplicateChecker.FindDuplicates(Logentries).Cast<LogEntryModel>());
            if (Logentries.Count == 0)
            {
                MessageBox.Show("Keine Duplikate vorhanden!");
                this.LoadLogentries();
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
    }
}
