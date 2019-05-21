using MonitoringClient.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;

namespace MonitoringClient.Repository
{
    public class LogentriesModelRepository : RepositoryBase<LogentriesModel>
    {
        public override string TableName { get; }
        public override ObservableCollection<LogentriesModel> Items { get; set; }
        public LogentriesModelRepository()
        {
            Items = new ObservableCollection<LogentriesModel>();
            TableName = "v_logentries";
        }
        public ObservableCollection<LogentriesModel> LoadLogentries()
        {
            this.Items.Clear();
            var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT id, pod, location, hostname, severity, timestamp, message FROM {this.TableName}";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Items.Add(new LogentriesModel(
                                reader.GetInt32("Id"),
                                reader.GetString("Pod"),
                                reader.GetValue(reader.GetOrdinal("Location")) as string,
                                reader.GetString("Hostname"),
                                reader.GetInt32("Severity"),
                                reader.GetString("Timestamp"),
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
            return Items;
        }
        public void ConfirmLogentries(int id)
        {
            var connection = new MySqlConnection(ConnectionString);
            try
            {
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
            var connection = new MySqlConnection(ConnectionString);
            try
            {
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

        public override LogentriesModel GetSingle<P>(P pkValue)
        {
            throw new NotImplementedException();
        }


        public override void Add(LogentriesModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(LogentriesModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(LogentriesModel entity)
        {
            throw new NotImplementedException();
        }

        public override List<LogentriesModel> GetAll(string whereCondition, Dictionary<string, object> parameterValues)
        {
            throw new NotImplementedException();
        }

        public override List<LogentriesModel> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
