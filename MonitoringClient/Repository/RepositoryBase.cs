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
    public abstract class RepositoryBase<M> : IRepositoryBase<M>
    {
        public abstract string TableName { get; }
        public abstract ObservableCollection<M> Items { get; set;}
        public string ConnectionString { get; set; } // Server = localhost; Database =inventarisierungsloesung; Uid = root; Pwd = password;

        protected RepositoryBase()
        {
            this.ConnectionString = "Server = localhost; Database = ; Uid = root; Pwd = ;";
        }

        public long Count(string whereCondition, Dictionary<string, object> parameterValues) // Funktioniert, 24.05.2019
        {
            try
            {
                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT COUNT(*) FROM {this.TableName} WHERE {whereCondition}";
                    foreach (KeyValuePair<string, object> entry in parameterValues)
                    {
                        cmd.Parameters.AddWithValue(entry.Key.ToString(), entry.Value);
                    }
                    return (long)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
                return 0;
            }

        }

        public long Count() // Funktioniert, 24.05.2019
        {
            try
            {
                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"select count(*) from {this.TableName}";
                    return (long)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
                return 0;
            }
        }

        public abstract M GetSingle<P>(P pkValue);

        public abstract void Add(M entity);

        public abstract void Delete(M entity);

        public abstract void Update(M entity);

        public abstract List<M> GetAll(string whereCondition, Dictionary<string, object> parameterValues);

        public abstract List<M> GetAll();

        public abstract IQueryable<M> Query(string whereCondition, Dictionary<string, object> parameterValues);
    }
}
