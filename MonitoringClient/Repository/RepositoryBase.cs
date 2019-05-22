using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringClient.Repository
{
    public abstract class RepositoryBase<M> : IRepositoryBase<M>
    {
        public abstract string TableName { get; }
        public abstract ObservableCollection<M> Items { get; set;}
        public string ConnectionString { get; set; } // Server = localhost; Database =inventarisierungsloesung; Uid = root; Pwd = password;

        protected RepositoryBase()
        {
            this.ConnectionString = "Server = localhost; Database = ; Uid = root; Pwd = ;";
        }

        public long Count(string whereCondition, Dictionary<string, object> parameterValues) // TODO
        {
            throw new NotImplementedException();
        }

        public long Count()
        {
            using (var conn = new MySqlConnection(this.ConnectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = $"select count(*) from {this.TableName}";
                    return (long)cmd.ExecuteScalar();
                }
            }
        }
        public abstract M GetSingle<P>(P pkValue);
        public abstract void Add(M entity);
        public abstract void Delete(M entity);

        public abstract void Update(M entity);

        public abstract List<M> GetAll(string whereCondition, Dictionary<string, object> parameterValues);

        public abstract List<M> GetAll();
    }
}
