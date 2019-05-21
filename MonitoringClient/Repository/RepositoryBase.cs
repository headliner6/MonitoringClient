using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringClient.Repository
{
    public abstract class RepositoryBase<M> : IRepositoryBase<M>
    {
        public string TableName { get; }
        public string ConnectionString { get; }

        public RepositoryBase()
        {
            this.ConnectionString = "Server = localhost; Database = ; Uid = root; Pwd = ;";
        }

        public long Count(string whereCondition, Dictionary<string, object> parameterValues)
        {
            throw new NotImplementedException();
        }

        public long Count()
        {
            throw new NotImplementedException();
        }
    }
}
