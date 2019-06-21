using MonitoringClient.Command;
using MonitoringClient.DataStructures;
using MonitoringClient.Repository;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MonitoringClient.ViewModel
{
    public class LocationViewModel : INotifyPropertyChanged, IViewModel
    {
        private readonly Action<object> navigateToLogEntryView;
        private ObservableCollection<LocationNode> _locations;
        private LocationModelRepository _locationModelRepository;

        public event PropertyChangedEventHandler PropertyChanged;
        public string ConnectionString { get; set; }
        public ObservableCollection<LocationNode> Locations
        {
            get { return _locations; }
            set
            {
                _locations = value;
                OnPropertyChanged("Locations");
            }
        }

        public GetAllLocationsCommand GetAllLocationsCommand { get; set; }
        public ICommand NavigateBack { get; set; }

        public LocationViewModel(Action<object> navigateToLogEntryView)
        {
            _locationModelRepository = new LocationModelRepository();
            NavigateBack = new BaseCommand(OnNavigateBack);
            this.navigateToLogEntryView = navigateToLogEntryView;

            GetAllLocationsCommand = new GetAllLocationsCommand(this);
        }
        public void GetAll()
        {
            _locationModelRepository.ConnectionString = ConnectionString;
            var locationTreeBuilder = new LocationTreeBuilder();
            var locations = _locationModelRepository.GetAll().ToList();
            locations.Sort((x, y) => x.ParentLocation.CompareTo(y.ParentLocation));
            Locations = locationTreeBuilder.BuildTree(locations);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void OnNavigateBack(object obj)
        {
            navigateToLogEntryView.Invoke("LogEntryView");
        }
    }
}
