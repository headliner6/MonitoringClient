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
    class LocationModelRepository : RepositoryBase<LocationModel>
    {
        private LocationModel _item;
        public override string TableName { get; }
        public override ObservableCollection<LocationModel> Items { get; set; }

        public LocationModelRepository()
        {
            Items = new ObservableCollection<LocationModel>();
            TableName = "Location";
        }
      
        public override LocationModel GetSingle<P>(P pkValue)
        {
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
                            _item = (new LocationModel(
                                reader.GetInt32("Id"),
                                reader.GetInt32("Adressnumber"),
                                reader.GetValue(reader.GetOrdinal("Designation")) as string,
                                reader.GetInt32("Building"),
                                reader.GetInt32("Rooom")
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
            return _item;
        }

        public override void Add(LocationModel entity)
        {
            try
            {
                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO {this.TableName} (adress_fk, designation, building, room) VALUES (@adress_fk, @designation, @building, @room)";
                    cmd.Parameters.AddWithValue("@adress_fk", entity.AdressNumber);
                    cmd.Parameters.AddWithValue("@designation", entity.Designation);
                    cmd.Parameters.AddWithValue("@building", entity.Building);
                    cmd.Parameters.AddWithValue("@room", entity.Room);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        }

        public override void Delete(LocationModel entity)
        {
            try
            {
                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"DELETE FROM {this.TableName} WHERE location_id = @location_id";
                    cmd.Parameters.AddWithValue("@location_id", entity.Id);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        }

        public override void Update(LocationModel entity)
        {
            try
            {
                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"UPDATE {this.TableName} SET adress_fk = @adress_fk, designation = @designation, building = @building, room = @room  WHERE location_id = @location_id";
                    cmd.Parameters.AddWithValue("@location_id", entity.Id);
                    cmd.Parameters.AddWithValue("@adress_fk", entity.AdressNumber);
                    cmd.Parameters.AddWithValue("@designation", entity.Designation);
                    cmd.Parameters.AddWithValue("@building", entity.Building);
                    cmd.Parameters.AddWithValue("@room", entity.Room);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        }

        public override List<LocationModel> GetAll()
        {
            var locations = new List<LocationModel>();
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
                            locations.Add(new LocationModel(
                                reader.GetInt32("Id"),
                                reader.GetInt32("Adressnumber"),
                                reader.GetValue(reader.GetOrdinal("Designation")) as string,
                                reader.GetInt32("Building"),
                                reader.GetInt32("Rooom")
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
            return locations;
        }

        public override List<LocationModel> GetAll(string whereCondition, Dictionary<string, object> parameterValues)
        {
            var locations = new List<LocationModel>();
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
                                locations.Add(new LocationModel(
                                    reader.GetInt32("Id"),
                                    reader.GetInt32("Adressnumber"),
                                    reader.GetValue(reader.GetOrdinal("Designation")) as string,
                                    reader.GetInt32("Building"),
                                    reader.GetInt32("Rooom")
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
            return locations;
        }

        public override IQueryable<LocationModel> Query(string whereCondition, Dictionary<string, object> parameterValues)
        {
            throw new NotImplementedException();
        }
    }
}
