using MonitoringClient.Model;
using MonitoringClient.Services;

namespace MonitoringClient.Repository
{
    public class CustomerRepository : RepositoryBase<CustomerModel>, ICustomerModelRepository
    {
        public override string TableName { get; }
        public CustomerRepository()
        {
            TableName = "customer";
        }
    }
}
