using MonitoringClient.Command;
using MonitoringClient.DataStructures;
using MonitoringClient.Model;
using MonitoringClient.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringClient.ViewModel
{
    public class LocationViewModel : INotifyPropertyChanged, IViewModel
    {
        private readonly Action<object> navigateToLogEntryView;
        private List<TreeNode<LocationModel>> _locations;
        private LocationModelRepository _locationModelRepository;

        public event PropertyChangedEventHandler PropertyChanged;
        public string ConnectionString { get; set; }
        public List<TreeNode<LocationModel>> Locations
        {
            get { return _locations; }
            set
            {
                _locations = value;
                OnPropertyChanged("Locations");
            }
        }

        public GetAllLocationsButtonCommand GetAllLocationsButtonCommand { get; set; }
        public BaseCommand NavigateBack { get; set; }

        public LocationViewModel(Action<object> navigateToLogEntryView)
        {
            _locationModelRepository = new LocationModelRepository();
            NavigateBack = new BaseCommand(OnNavigateBack);
            this.navigateToLogEntryView = navigateToLogEntryView;

            GetAllLocationsButtonCommand = new GetAllLocationsButtonCommand(this);
        }
        public void GetAll()
        {
            _locationModelRepository.ConnectionString = ConnectionString;
            var locationTreeBuilder = new LocationTreeBuilder();
            var locations = _locationModelRepository.GetAll();
            locations.Sort((x, y) => x.ParentLocation.CompareTo(y.ParentLocation));
            _locations = new List<TreeNode<LocationModel>>(locationTreeBuilder.BuildTree(locations));
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void OnNavigateBack(object obj)
        {
            navigateToLogEntryView.Invoke("Locations");
        }
    }
}
