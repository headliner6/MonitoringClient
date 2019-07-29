using MonitoringClient.Model;
using MonitoringClient.Repository.Context;
using System;
using System.Data.Entity;
using System.Windows;

namespace MonitoringClient.Repository
{
    public class LogEntryModelRepository : RepositoryBase<V_logentries>
    {

        public void ConfirmLogentries(int id)
        {
            try
            {
                using (var context = new InventarisierungsloesungDB(ConnectionString))
                {
                    context.LogClear(id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        }

        public void AddMessage(string pod, string hostname, string severity, string message)
        {
            try
            {
                using (var context = new InventarisierungsloesungDB(ConnectionString))
                {
                    context.LogMessageAdd(pod, hostname, severity, message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        }
        public override void Add(V_logentries entity)
        {
            throw new NotSupportedException();
        }

        public override void Delete(V_logentries entity)
        {
            throw new NotSupportedException();
        }

        public override void Update(V_logentries entity)
        {
            throw new NotSupportedException();
        }
    }
}
