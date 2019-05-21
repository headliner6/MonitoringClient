using DuplicateCheckerLib;
using MonitoringClient.Model;
using MonitoringClient.Repository;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Input;

namespace MonitoringClient.ViewModel
{
    public class LogentriesViewModel : INotifyPropertyChanged
    {
        private readonly Action<object> navigate;
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
        public LoadButtonCommand LoadButtonCommand { get; set; }
        public ConfirmButtonCommand ConfirmButtonCommand { get; set; }
        public FindDuplicatesButtonCommand FindDuplicatesButtonCommand { get; set; }

        public LogentriesViewModel(Action<object> navigate)
        {
            Navigate = new BaseCommand(OnNavigate);
            this.navigate = navigate;

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
            var enumerableListeOfDuplicates = _duplicateChecker.FindDuplicates(Logentries);
            Logentries = new ObservableCollection<LogentriesModel>(enumerableListeOfDuplicates.Cast<LogentriesModel>());
        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void OnNavigate(object obj)
        {
            navigate.Invoke("LogMessageAddView");
        }
    }
}
