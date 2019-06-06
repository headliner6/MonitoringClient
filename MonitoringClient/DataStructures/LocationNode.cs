using MonitoringClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringClient.DataStructures
{
    public class LocationNode
    {
        public LocationsModel Element { get; set; }
        public List<LocationNode> ChildeNodes { get; set; }
    }
}
