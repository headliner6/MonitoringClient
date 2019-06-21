using MonitoringClient.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MonitoringClient.Repository
{
    public abstract class RepositoryBase<M> : IRepositoryBase<M>
        where M : IModel, new()
    {
        public abstract string TableName { get; }
        public string ConnectionString { get; set; } // "Server = localhost; Database = inventarisierungsloesung; Uid = root; Pwd = password;"
        public abstract string PrimaryKey { get; }
        public abstract string InsertIntoEntityFieldForSqlStatement { get; }

        protected RepositoryBase()
        {
            this.ConnectionString = "Server = localhost; Database = ; Uid = root; Pwd = ;";
        }

        public abstract List<M> GetEntitiesFromDB(MySqlDataReader reader);
        public abstract M GetEntityFromDB(MySqlDataReader reader);
        public abstract string UpdateSqlStatementValues(M entity);
        public abstract string AddSqlStatementValues(M entity);

        public long Count(string whereCondition, Dictionary<string, object> parameterValues)
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
                throw ex;
            }

        }

        public long Count()
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
                throw ex;
            }
        }

        public M GetSingle<P>(P pkValue)
        {
            var entity = new M();
            try
            {
                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {this.TableName} WHERE {this.PrimaryKey} = @primaryKey;";
                    cmd.Parameters.AddWithValue("@primaryKey", pkValue);
                    using (var reader = cmd.ExecuteReader())
                    {
                            entity = GetEntityFromDB(reader);
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
            return entity;
        }

        public virtual void Add(M entity)
        {
            try
            {
                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO {this.TableName}" +
                        $" ({InsertIntoEntityFieldForSqlStatement}) "  +
                        $"VALUES ({AddSqlStatementValues(entity)});";

                    var finish = cmd.ExecuteNonQuery();
                    if (finish == 1)
                    {
                        MessageBox.Show("Hinzufuegen war erfolgreich!");
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        }

        public virtual void Delete(M entity)
        {
            try
            {
                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"DELETE FROM {this.TableName} WHERE {PrimaryKey} = {entity.Id}";
                    var finish = cmd.ExecuteNonQuery();
                    if (finish == 1)
                    {
                        MessageBox.Show("Loeschen war erfolgreich!");
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        }

        public virtual void Update(M entity)
        {
            try
            {
                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"UPDATE {this.TableName} SET {UpdateSqlStatementValues(entity)}  WHERE {PrimaryKey} = {entity.Id};";
                    var finish = cmd.ExecuteNonQuery();
                    if (finish == 1)
                    {
                        MessageBox.Show("Update war erfolgreich!");
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        }

        public List<M> GetAll(string whereCondition, Dictionary<string, object> parameterValues)
        {
            var entities = new List<M>();
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
                            entities = GetEntitiesFromDB(reader);
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
                }
            }
            return entities;
        }

        public List<M> GetAll()
        {
            var entities = new List<M>();
            try
            {
                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {this.TableName}";
                    using (var reader = cmd.ExecuteReader())
                    {
                        entities = GetEntitiesFromDB(reader);
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
            return entities;
        }
    }
}
