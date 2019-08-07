using MonitoringClient.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MonitoringClient.Repository
{
    public class LocationModelRepository : RepositoryBase<Location>
    {
        public List<Location> GetLocationsHirarchical()
        {
            var locations = new List<Location>();
            var context = new InventarisierungsloesungEntities();
            var locationsHirarchical = context.GetLocationsHirarchicalRecursive();
            foreach (var location in locationsHirarchical)
            {
                var tempLocation = new Location();
                tempLocation.Id = location.Id;
                tempLocation.ParentLocation = location.ParentLocation;
                tempLocation.Addressnumber = location.Addressnumber;
                tempLocation.Designation = location.Designation;
                tempLocation.Building = location.Building;
                tempLocation.Room = location.Room;
                locations.Add(tempLocation);
            }
            return locations;
        }
    }
}
