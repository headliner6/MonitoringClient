using MonitoringClient.Model;

namespace MonitoringClient.Repository
{
    public class CustomerRepository : RepositoryBase<CustomerModel>
    {
        public override string TableName { get; }
        public CustomerRepository()
        {
            TableName = "customer";
        }
    }
}
