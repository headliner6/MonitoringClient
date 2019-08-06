using MonitoringClient.Model;
using System.Data.Entity;
using System.Linq;

namespace MonitoringClient.Repository
{
    public class LocationModelRepository : RepositoryBase<Location>
    {
        public IQueryable<Location> GetLocationsHirarchical()
        {
            var context = new InventarisierungsloesungEntities();
            return context.GetLocationsHirarchicalRecursive();
        }
    }
}
