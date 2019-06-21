using MonitoringClient.Model;

namespace MonitoringClient.Repository
{
    class LocationModelRepository : RepositoryBase<LocationModel>
    {
        public override string TableName { get; }
        public LocationModelRepository()
        {
            TableName = "location";
        }
    }
}
