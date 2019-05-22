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

        public override LogentriesModel GetSingle<P>(P pkValue) // TODO
        {
            throw new NotImplementedException();
        }
        public override void Add(LogentriesModel entity)
        {
            var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO {this.TableName} (Pod, Location, Hostname, Severity, Message) VALUES (@id, @pod, @location, @hostname, @severity, @message)";
                    cmd.Parameters.AddWithValue("@pod", entity.Pod);
                    cmd.Parameters.AddWithValue("@location", entity.Location);
                    cmd.Parameters.AddWithValue("@hostname", entity.Hostname);
                    cmd.Parameters.AddWithValue("@severity", entity.Severity);
                    cmd.Parameters.AddWithValue("@message", entity.Message);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        }
        public override void Delete(LogentriesModel entity)
        {
            MessageBox.Show("Delete steht nicht zur Verfügung");
        }
        public override void Update(LogentriesModel entity)
        {
            MessageBox.Show("Update steht nicht zur Verfügung");
        }
        public override List<LogentriesModel> GetAll(string whereCondition, Dictionary<string, object> parameterValues) // TODO
        {
            var logentries = new List<LogentriesModel>();
            var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {this.TableName} WHERE {whereCondition}";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logentries.Add(new LogentriesModel(
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
            return logentries;
        }
        public override List<LogentriesModel> GetAll()
        {
            var logentries = new List<LogentriesModel>();
            var connection = new MySqlConnection(ConnectionString);

            try
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {this.TableName}";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logentries.Add(new LogentriesModel(
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
            return logentries;
        }
    }
}
