using LinqToDB.Data;
using MonitoringClient.Model;
using MonitoringClient.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace MonitoringClient.Repository
{
    public class LogEntryModelRepository : RepositoryBase<LogEntryModel>, ILogEntryModelRepository
    {
        public override string TableName { get; }
        public LogEntryModelRepository()
        {
            TableName = "v_logentries";
        }

        public void ConfirmLogentries(int id)
        {
            try
            {
                using (var dataConnection = new DataConnection(DbProvider, ConnectionString))
                {   
                    dataConnection.QueryProc<LogEntryModel>("LogClear", new DataParameter("@_logentries_id", id));
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
                using (var dataConnection = new DataConnection(DbProvider, ConnectionString))
                {
                    dataConnection.QueryProc<LogEntryModel>("LogMessageAdd",
                        new DataParameter("@i_pod", pod),
                        new DataParameter("@i_hostname", hostname),
                        new DataParameter("@i_severity", severity),
                        new DataParameter("@i_message", message));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        }
        public override void Add(LogEntryModel entity)
        {
            throw new NotSupportedException();
        }

        public override void Delete(LogEntryModel entity)
        {
            throw new NotSupportedException();
        }

        public override void Update(LogEntryModel entity)
        {
            throw new NotSupportedException();
        }
    }
}
