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

        public override LogEntryModel GetSingle<P>(P pkValue)
        {
            var item = new LogEntryModel();
            try
            {
                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {this.TableName} WHERE id = @primaryKey";
                    cmd.Parameters.AddWithValue("@primaryKey", pkValue);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item = (new LogEntryModel(
                                reader.GetInt32("Id"),
                                reader.GetString("Pod"),
                                reader.GetValue(reader.GetOrdinal("Location")) as string,
                                reader.GetString("Hostname"),
                                reader.GetInt32("Severity"),
                                reader.GetDateTime("Timestamp"),
                                reader.GetString("Message")
                                ));
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
            return item;
        }

        public override void Add(LogEntryModel entity)
        {
            MessageBox.Show("Add Methode steht bei einer View nicht zur Verfügung!");
        }

        public override void Delete(LogEntryModel entity)
        {
            MessageBox.Show("Delete Methode steht bei einer View nicht zur Verfügung!");
        }
        public override void Update(LogEntryModel entity)
        {
            MessageBox.Show("Update Methode steht bei einer View nicht zur Verfügung!");
        }

        public override List<LogEntryModel> GetAll()
        {
            var logentries = new List<LogEntryModel>();
            try
            {
                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {this.TableName}";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logentries.Add(new LogEntryModel(
                                reader.GetInt32("Id"),
                                reader.GetString("Pod"),
                                reader.GetValue(reader.GetOrdinal("Location")) as string,
                                reader.GetString("Hostname"),
                                reader.GetInt32("Severity"),
                                reader.GetDateTime("Timestamp"),
                                reader.GetString("Message")
                                ));
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
            return logentries;
        }

        public override List<LogEntryModel> GetAll(string whereCondition, Dictionary<string, object> parameterValues)
        {
            var logentries = new List<LogEntryModel>();
            if (string.IsNullOrEmpty(whereCondition))
            {
                MessageBox.Show("WhereCondition darf nicht leer sein!");
            }
            else
            {
                try
                {
                    var connection = new MySqlConnection(ConnectionString);
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = $"SELECT * FROM {this.TableName} WHERE {whereCondition}";
                        foreach (KeyValuePair<string, object> entry in parameterValues)
                        {
                            cmd.Parameters.AddWithValue(entry.Key.ToString(), entry.Value);
                        }
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                logentries.Add(new LogEntryModel(
                                    reader.GetInt32("Id"),
                                    reader.GetString("Pod"),
                                    reader.GetValue(reader.GetOrdinal("Location")) as string,
                                    reader.GetString("Hostname"),
                                    reader.GetInt32("Severity"),
                                    reader.GetDateTime("Timestamp"),
                                    reader.GetString("Message")
                                ));
                            }
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
                }
            }
            return logentries;
        }
    }
}
