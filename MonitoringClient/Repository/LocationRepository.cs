using MonitoringClient.Model;
using MonitoringClient.Services;

namespace MonitoringClient.Repository
{
    public class LocationRepository : RepositoryBase<LocationModel>, ILocationRepository
    {
        public override string TableName { get; }
        public LocationRepository()
        {
            TableName = "location";
        }
    }
}
