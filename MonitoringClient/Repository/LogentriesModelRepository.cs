using MonitoringClient.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    cmd.CommandText = "SELECT id, pod, location, hostname, severity, timestamp, message FROM v_logentries";
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
    }
}
