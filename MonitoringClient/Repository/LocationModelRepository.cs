using MonitoringClient.Model;
using MonitoringClient.DataStructures;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;

namespace MonitoringClient.Repository
{
    class LocationModelRepository : RepositoryBase<LocationModel>
    {
        public override string TableName { get; }

        public LocationModelRepository()
        {
            TableName = "Location";
        }
      
        public override LocationModel GetSingle<P>(P pkValue)
        {
            var item = new LocationModel();
            try
            {
                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {this.TableName} WHERE location_id = @primaryKey";
                    cmd.Parameters.AddWithValue("@primaryKey", pkValue);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item = (new LocationModel(
                                reader.GetInt32("location_id"),
                                reader.GetInt32("parent_location"),
                                reader.GetInt32("address_fk"),
                                reader.GetValue(reader.GetOrdinal("designation")) as string,
                                reader.GetInt32("building"),
                                reader.GetInt32("room")
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

        public override void Add(LocationModel entity)
        {
            try
            {
                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO {this.TableName} (parent_location, address_fk, designation, building, room) VALUES (@parent_location, @address_fk, @designation, @building, @room)";
                    cmd.Parameters.AddWithValue("@parent_location", entity.ParentLocation);
                    cmd.Parameters.AddWithValue("@address_fk", entity.Addressnumber);
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
                    cmd.CommandText = $"UPDATE {this.TableName} SET parent_location = @parent_location, address_fk = @address_fk, designation = @designation, building = @building, room = @room  WHERE location_id = @location_id";
                    cmd.Parameters.AddWithValue("@location_id", entity.Id);
                    cmd.Parameters.AddWithValue("@parent_location", entity.ParentLocation);
                    cmd.Parameters.AddWithValue("@address_fk", entity.Addressnumber);
                    cmd.Parameters.AddWithValue("@designation", entity.Designation);
                    cmd.Parameters.AddWithValue("@building", entity.Building);
                    cmd.Parameters.AddWithValue("@room", entity.Room);
                    var finish = cmd.ExecuteNonQuery();
                    if (finish == 1)
                    {
                        MessageBox.Show("Update erfolgreich!");
                    }
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
                                reader.GetInt32("location_id"),
                                reader.GetInt32("parent_location"),
                                reader.GetInt32("address_fk"),
                                reader.GetValue(reader.GetOrdinal("designation")) as string,
                                reader.GetInt32("building"),
                                reader.GetInt32("room")
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
                                    reader.GetInt32("location_id"),
                                    reader.GetInt32("parent_location"),
                                    reader.GetInt32("address_fk"),
                                    reader.GetValue(reader.GetOrdinal("designation")) as string,
                                    reader.GetInt32("building"),
                                    reader.GetInt32("room")
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
    }
}
