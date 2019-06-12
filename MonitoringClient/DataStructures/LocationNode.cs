using MonitoringClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void AddChildNode(LocationModel element)
        {
            this.ChildNodes.Add(new LocationNode(element));
        }
    }
}
