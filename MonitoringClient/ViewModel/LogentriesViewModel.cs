using DuplicateCheckerLib;
using MonitoringClient.Model;
using MonitoringClient.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using static MonitoringClient.ViewModel.NavigationViewModel;

namespace MonitoringClient.ViewModel
{
    public class LogentriesViewModel : INotifyPropertyChanged
    {
        private readonly Action<object> navigate;
        private LoadButtonCommand _loadButtonCommand;
        private ConfirmButtonCommand _confirmButtonCommand;
        private FindDuplicatesButtonCommand _findDuplicatesButtonCommand;
        private DuplicateChecker _duplicateChecker;
        private LogentriesModelRepository _logentriesModelRepository;
        private ObservableCollection<LogentriesModel> _logentries;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand Navigate { get; set; }
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
        public LoadButtonCommand LoadButtonCommand
        {
            get { return this._loadButtonCommand; }
            set { this._loadButtonCommand = value; }
        }
        public ConfirmButtonCommand ConfirmButtonCommand
        {
            get { return this._confirmButtonCommand; }
            set { this._confirmButtonCommand = value; }
        }
        public FindDuplicatesButtonCommand FindDuplicatesButtonCommand
        {
            get { return this._findDuplicatesButtonCommand; }
            set { this._findDuplicatesButtonCommand = value; }
        }

        public LogentriesViewModel(Action<object> navigate)
        {
            Navigate = new BaseCommand(OnNavigate);
            this.navigate = navigate;

            _logentriesModelRepository = new LogentriesModelRepository();

            ConnectionString = _logentriesModelRepository.ConnectionString;
            _loadButtonCommand = new LoadButtonCommand(this);
            _confirmButtonCommand = new ConfirmButtonCommand(this);
            Logentries = new ObservableCollection<LogentriesModel>();
            _duplicateChecker = new DuplicateChecker();
            _findDuplicatesButtonCommand = new FindDuplicatesButtonCommand(this);
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
            var enumerableListeOfDuplicates = _duplicateChecker.FindDuplicates(Logentries);
            Logentries = new ObservableCollection<LogentriesModel>(enumerableListeOfDuplicates.Cast<LogentriesModel>());
        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        private void OnNavigate(object obj)
        {
            navigate.Invoke("LogMessageAddView");
        }
    }
}
