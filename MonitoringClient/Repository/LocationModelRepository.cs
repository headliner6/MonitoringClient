using MonitoringClient.Model;
using MonitoringClient.Services;

namespace MonitoringClient.Repository
{
    class LocationModelRepository : RepositoryBase<LocationModel>, ILocationModelRepository
    {
        public override string TableName { get; }
        public LocationModelRepository()
        {
            TableName = "location";
        }
    }
}
