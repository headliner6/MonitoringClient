using MonitoringClient.Model;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace MonitoringClient.Repository.Context
{
    public partial class InventarisierungsloesungDB : DbContext
    {
        public InventarisierungsloesungDB(string connectionString)
            : base(connectionString)
        {
        }

        public virtual DbSet<V_logentries> V_Logentries { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Kundenkonto> Kundenkonto { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //throw new UnintentionalCodeFirstException();
        }

        public virtual ObjectResult<V_logentries> LogClear(int id)
        {
            var logentryIdParameter = new ObjectParameter("logentries_id", id);

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<V_logentries>("LogClear", logentryIdParameter);
        }

        public virtual int LogMessageAdd(string pod, string hostname, string severity, string message)
        {
            var parameters = new ObjectParameter[]
            {
                new ObjectParameter("i_pod", pod),
                new ObjectParameter("i_hostname", hostname),
                new ObjectParameter("i_severity", severity),
                new ObjectParameter("i_message", message)
            };

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("LogMessageAdd", parameters);
        }

    }
}
