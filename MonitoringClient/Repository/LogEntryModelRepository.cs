using MonitoringClient.Model;
using System;
using System.Data.Entity;
using System.Windows;

namespace MonitoringClient.Repository
{
    public class LogEntryModelRepository : RepositoryBase<LogEntries>
    {

        public void ConfirmLogentries(int id)
        {
            try
            {
                var context = new InventarisierungsloesungEntities();

                context.LogClear(id);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        }

        public void AddMessage(string pod, string hostname, int? severity, string message)
        {
            try
            {
                var context = new InventarisierungsloesungEntities();

                context.LogMessageAdd(pod, hostname, severity, message);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        }
        public override void Add(LogEntries entity)
        {
            throw new NotSupportedException();
        }

        public override void Delete(LogEntries entity)
        {
            throw new NotSupportedException();
        }

        public override void Update(LogEntries entity)
        {
            throw new NotSupportedException();
        }
    }
}
