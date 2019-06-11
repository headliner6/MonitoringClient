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

namespace MonitoringClient.ViewModel
{
    public class LocationViewModel : INotifyPropertyChanged, IViewModel
    {
        private readonly Action<object> navigateToLogentriesView;
        private ObservableCollection<LocationModel> _locations;

        private LocationModelRepository _locationModelRepository;

        public event PropertyChangedEventHandler PropertyChanged;

        public BaseCommand NavigateBack { get; set; }
        public string ConnectionString { get; set; }
        public ObservableCollection<LocationModel> Locations
        {
            get { return _locations; }
            set
            {
                _locations = value;
                OnPropertyChanged("Logentries");
            }
        }

        private void OnPropertyChanged(string v)
        {
            throw new NotImplementedException();
        }

        public LocationViewModel(Action<object> navigateToLogentriesView)
        {
            _locationModelRepository = new LocationModelRepository();
            NavigateBack = new BaseCommand(OnNavigateBack);
            this.navigateToLogentriesView = navigateToLogentriesView;
        }

        private void OnNavigateBack(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
