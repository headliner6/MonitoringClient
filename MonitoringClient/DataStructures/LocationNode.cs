using MonitoringClient.Model;
using System.Collections.ObjectModel;

namespace MonitoringClient.DataStructures
{
    public class LocationNode
    {
        public LocationModel Location { get; set; }
        public ObservableCollection<LocationNode> ChildNodes { get; set; }

        public LocationNode(LocationModel location)
        {
            this.Location = location;
            this.ChildNodes = new ObservableCollection<LocationNode>();
        }
    }
}
