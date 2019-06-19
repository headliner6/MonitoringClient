using LinqToDB;
using MonitoringClient.Model;
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
        where M : class, IModel, new()
    {
        public abstract string TableName { get; }
        public string ConnectionString { get; set; } // "Server = localhost; Database = inventarisierungsloesung; Uid = root; Pwd = password;"
        public abstract string PrimaryKey { get; }
        public abstract string InsertIntoEntityFieldForSqlStatement { get; }
        private string _dataBase { get; }
        private string _dbProvider { get; }

        protected RepositoryBase()
        {
            this.ConnectionString = "Server = localhost; Database = inventarisierungsloesung; Uid = root; Pwd = password;"/*"Server = localhost; Database = ; Uid = root; Pwd = ;"*/;
            this._dataBase = "inventarisierungsloesung";
            this._dbProvider = "MySql";
        }

        public abstract List<M> GetEntitiesFromDB(MySqlDataReader reader);
        public abstract M GetEntityFromDB(MySqlDataReader reader);
        public abstract string UpdateSqlStatementValues(M entity);
        public abstract string AddSqlStatementValues(M entity);

        public long Count(string whereCondition, Dictionary<string, object> parameterValues)
        {
            if (string.IsNullOrEmpty(whereCondition))
            {
                MessageBox.Show("WhereCondition darf nicht leer sein!");
            }
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

        } // funktioniert, 16.06.2019

        public long Count()
        {
            try
            {
                using (var context = new DataContext(_dbProvider, ConnectionString))
                {
                    var table = context.GetTable<M>();
                    return table.Count();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
                return 0;
            }
        } // funktioniert, 16.06.2019 inkl. LINQ

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
        }// funktioniert, 16.06.2019

        public virtual void Add(M entity)
        {
            try
            {
                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO {this.TableName}" +
                        $" ({InsertIntoEntityFieldForSqlStatement}) " +
                        $"VALUES ({AddSqlStatementValues(entity)});";

                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        } // funktioniert, 16.06.2019

        public virtual void Delete(M entity)
        {
            try
            {
                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"DELETE FROM {this.TableName} WHERE {PrimaryKey} = {entity.Id}";
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        } // funktioniert, 16.06.2019

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
                        MessageBox.Show("Update erfolgreich!");
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        } // funktioniert, 16.06.2019

        public IQueryable<M> GetAll(string whereCondition, Dictionary<string, object> parameterValues)
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
            return entities.AsQueryable();
        } // funktioniert, 16.06.2019

        public IQueryable<M> GetAll()
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
            return entities.AsQueryable();
        } // funktioniert, 16.06.2019
        
    }
}
