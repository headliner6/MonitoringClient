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
    public class LogentriesViewModel : INotifyPropertyChanged, IViewModel
    {
        private readonly Action<object> navigateToLogMessageAddView;
        private readonly Action<object> navigateToLocationsView;
        private DuplicateChecker _duplicateChecker;
        private LogentriesModelRepository _logentriesModelRepository;
        private ObservableCollection<LogentriesModel> _logentries;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand NavigateLogMessageAddView { get; set; }
        public ICommand NvaigateLocationsView { get; set; }
        public ObservableCollection<LogentriesModel> Logentries
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

        public LogentriesViewModel(Action<object> navigateToLogMessageAddView, Action<object> navigateToLocationsView)
        {
            NavigateLogMessageAddView = new BaseCommand(StartLogMessageAddView);
            NvaigateLocationsView = new BaseCommand(StartLocationsView);
            this.navigateToLogMessageAddView = navigateToLogMessageAddView;
            this.navigateToLocationsView = navigateToLocationsView;

            _logentriesModelRepository = new LogentriesModelRepository();

            ConnectionString = _logentriesModelRepository.ConnectionString;
            LoadButtonCommand = new LoadButtonCommand(this);
            ConfirmButtonCommand = new ConfirmButtonCommand(this);
            Logentries = new ObservableCollection<LogentriesModel>();
            _duplicateChecker = new DuplicateChecker();
            FindDuplicatesButtonCommand = new FindDuplicatesButtonCommand(this);
        }
        public void LoadLogentries()
        {
            _logentriesModelRepository.ConnectionString = ConnectionString;
            Logentries = _logentriesModelRepository.LoadLogentries();
        }
        public void ConfirmLogentries(int id)
        {
            _logentriesModelRepository.ConfirmLogentries(id);
        }
        public void CheckForDuplicates()
        {
            this.LoadLogentries();
            Logentries = new ObservableCollection<LogentriesModel>(_duplicateChecker.FindDuplicates(Logentries).Cast<LogentriesModel>());
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
        private void StartLocationsView(object obj)
        {
            navigateToLocationsView.Invoke("LocationsView");
        }
    }
}
