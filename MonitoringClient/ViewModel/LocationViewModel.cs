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
using System.Windows;
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

        public ICommand NavigateBack { get; set; }

        public LocationViewModel(Action<object> navigateToLogEntryView)
        {
            _locationModelRepository = new LocationModelRepository();
            NavigateBack = new BaseCommand(OnNavigateBack);
            this.navigateToLogEntryView = navigateToLogEntryView;
        }
        public void GetAll()
        {
            try
            {
                _locationModelRepository.ConnectionString = ConnectionString;
                var locationTreeBuilder = new LocationTreeBuilder();
                var locations = _locationModelRepository.GetAll().ToList();
                locations.Sort((x, y) => x.ParentLocation.CompareTo(y.ParentLocation));
                Locations = locationTreeBuilder.BuildTree(locations);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
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
