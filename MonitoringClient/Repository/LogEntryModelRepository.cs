using MonitoringClient.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;

namespace MonitoringClient.Repository
{
    public class LogEntryModelRepository : RepositoryBase<LogEntryModel>
    {
        public override string TableName { get; }
        public override string PrimaryKey { get { return "id"; } }
        public override string InsertIntoEntityFieldForSqlStatement { get { return "pod, location, hostname, severity, timestamp, message"; } }

        public LogEntryModelRepository()
        {
            TableName = "v_logentries";
        }

        public void ConfirmLogentries(int id)
        {
            try
            {
                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "LogClear";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@_logentries_id", id);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
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
                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "LogMessageAdd";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@i_pod", pod);
                    cmd.Parameters.AddWithValue("@i_hostname", hostname);
                    cmd.Parameters.AddWithValue("@i_severity", severity);
                    cmd.Parameters.AddWithValue("@i_message", message);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        }

        public override LogEntryModel GetEntityFromDB(MySqlDataReader reader)
        {
            var logEntry = new LogEntryModel();
            while (reader.Read())
            {
                logEntry = new LogEntryModel(
                    reader.GetInt32("Id"),
                    reader.GetString("Pod"),
                    reader.GetValue(reader.GetOrdinal("Location")) as string,
                    reader.GetString("Hostname"),
                    reader.GetInt32("Severity"),
                    reader.GetDateTime("Timestamp"),
                    reader.GetString("Message")
                );
            }
            return logEntry;
        }

        public override List<LogEntryModel> GetEntitiesFromDB(MySqlDataReader reader)
        {
            var logEntries = new List<LogEntryModel>();
            while (reader.Read())
            {
                logEntries.Add(new LogEntryModel(
                    reader.GetInt32("Id"),
                    reader.GetString("Pod"),
                    reader.GetValue(reader.GetOrdinal("Location")) as string,
                    reader.GetString("Hostname"),
                    reader.GetInt32("Severity"),
                    reader.GetDateTime("Timestamp"),
                    reader.GetString("Message")
                ));
            }
            return logEntries;
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

        public override string UpdateSqlStatementValues(LogEntryModel entity)
        {
            return $"pod = '{entity.Pod}', location = '{entity.Location}', hostname = '{entity.Hostname}', severity = {entity.Severity}, timestamp = {entity.Timestamp}, message = '{entity.Message}'";
        }

        public override string AddSqlStatementValues(LogEntryModel entity)
        {
            return $"pod = '{entity.Pod}', location = '{entity.Location}', hostname = '{entity.Hostname}', severity = {entity.Severity}, timestamp = {entity.Timestamp}, message = '{entity.Message}'";
        }
    }
}
