using DuplicateCheckerLib;
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
    public class LogentriesViewModel : INotifyPropertyChanged
    {
        private LocationModelRepository _locationModelRepository;
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
        public TestButtonCommand TestButtonCommand { get; set; }

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
            TestButtonCommand = new TestButtonCommand(this);
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
        private void OnNavigate(object obj)
        {
            navigate.Invoke("LogMessageAddView");
        }

        public void Test()
        {

            //// Test LocationRepo
            //_locationModelRepository = new LocationModelRepository();
            //_locationModelRepository.ConnectionString = ConnectionString;
            //var e = new LocationModel(2, 3, "Test", 6, 6);
            //_locationModelRepository.Update(e);

            ////Test LogentriesRepo
            //_locationModelRepository = new LocationModelRepository();
            //MessageBox.Show("" + _locationModelRepository.Count());
            //var dic = new Dictionary<string, object>
            //{
            //    { "id", 2 }
            //};
            //MessageBox.Show("" + _logentriesModelRepository.Count("Id = @Id", dic));

            //var a = _logentriesModelRepository.GetAll();
            //foreach (LogentriesModel lm in a)
            //{
            //    MessageBox.Show(lm.Hostname)
            //}
            //LogentriesModel lm = new LogentriesModel(66, "podtest", "locationtest", "hostnametest", 666, DateTime.Now, "messagetest");
            //_logentriesModelRepository.Update(lm);
            //_logentriesModelRepository.Delete(lm);

            //var b = _logentriesModelRepository.GetAll("Id > @Id", dic);
            //foreach (LogentriesModel lm in b)
            //{
            //    MessageBox.Show(lm.Hostname);
            //}

            //_logentriesModelRepository.Add(lm);
            //var lm = _logentriesModelRepository.GetSingle<int>(1);
            //MessageBox.Show(lm.Hostname);
        }
    }
}
