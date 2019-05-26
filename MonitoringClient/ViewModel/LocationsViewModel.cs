using MonitoringClient.Command;
using MonitoringClient.Model;
using MonitoringClient.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonitoringClient.ViewModel
{
    public class LocationsViewModel : INotifyPropertyChanged, IViewModel
    {
        private readonly Action<object> navigateToLogentriesView;
        private ObservableCollection<LocationsModel> _locations;

        private LocationModelRepository _locationModelRepository;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand NavigateBack { get; set; }
        public string ConnectionString { get; set; }
        public ObservableCollection<LocationsModel> Locations
        {
            get { return _locations; }
            set
            {
                _locations = value;
                OnPropertyChanged("Logentries");
            }
        }

        public GetSingleButtonCommand GetSingleButtonCommand { get; set; }
        public AddButtonCommand AddButtonCommand { get; set; }
        public DeleteButtonCommand DeleteButtonCommand { get; set; }
        public UpdateButtonCommand UpdateButtonCommand { get; set; }
        public GetAllButtonCommand GetAllButtonCommand { get; set; }
        public GetAllCorrectButtonCommand GetAllCorrectButtonCommand { get; set; }

        public LocationsViewModel(Action<object> navigateToLogentriesView)
        {
            _locationModelRepository = new LocationModelRepository();
            NavigateBack = new BaseCommand(OnNavigateBack);
            this.navigateToLogentriesView = navigateToLogentriesView;

            GetSingleButtonCommand = new GetSingleButtonCommand(this);
            AddButtonCommand = new AddButtonCommand(this);
            DeleteButtonCommand = new DeleteButtonCommand(this);
            UpdateButtonCommand = new UpdateButtonCommand(this);
            GetAllButtonCommand = new GetAllButtonCommand(this);
            GetAllCorrectButtonCommand = new GetAllCorrectButtonCommand(this);
        }

        public void GetAll()
        {
            _locationModelRepository.ConnectionString = ConnectionString;
            Locations = new ObservableCollection<LocationsModel>(_locationModelRepository.GetAll().Cast<LocationsModel>());
        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void OnNavigateBack(object obj)
        {
            navigateToLogentriesView.Invoke("LogentriesView");
        }
    }
}
