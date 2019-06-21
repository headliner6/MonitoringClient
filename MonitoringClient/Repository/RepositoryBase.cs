using LinqToDB;
using MonitoringClient.Model;
using MonitoringClient.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
=======
>>>>>>> 1b75c64bf9613056678978c0e6f12a0a5832905f
using System.Windows;

namespace MonitoringClient.Repository
{
    public abstract class RepositoryBase<M> : IRepositoryBase<M>
        where M : class, IModel, new()
    {
        public abstract string TableName { get; }
<<<<<<< HEAD
        public string ConnectionString { get; set; } // "Server = localhost; Database = inventarisierungsloesung; Uid = root; Pwd = password;"
        public string DbProvider { get; }
=======
        public string ConnectionString { get; set; }
        public abstract string PrimaryKey { get; }
        public abstract string InsertIntoEntityFieldForSqlStatement { get; }
>>>>>>> 1b75c64bf9613056678978c0e6f12a0a5832905f

        protected RepositoryBase()
        {
            this.ConnectionString = "Server = localhost; Database = inventarisierungsloesung; Uid = root; Pwd = password;"/*"Server = localhost; Database = ; Uid = root; Pwd = ;"*/;
            this.DbProvider = "MySql";
        }

        public abstract List<M> GetEntitiesFromDB(MySqlDataReader reader);
<<<<<<< HEAD
        public long Count(Expression <Func<M, bool>> whereCondition)
=======
        public abstract M GetEntityFromDB(MySqlDataReader reader);
        public abstract string UpdateSqlStatementValues(M entity);
        public abstract string AddSqlStatementValues(M entity);

        public long Count(string whereCondition, Dictionary<string, object> parameterValues)
>>>>>>> 1b75c64bf9613056678978c0e6f12a0a5832905f
        {
            if (string.IsNullOrEmpty(whereCondition))
            {
                MessageBox.Show("WhereCondition darf nicht leer sein!");
            }
            try
            {
                using (var context = new DataContext(DbProvider, ConnectionString))
                {
                    return context.GetTable<M>().Where(whereCondition).Count();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
                throw ex;
            }
        } // funktioniert, 21.06.2019 inkl. LINQ

        public long Count()
        {
            try
            {
                using (var context = new DataContext(DbProvider, ConnectionString))
                {
                   return context.GetTable<M>().Count();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
                throw ex;
            }
        } // funktioniert, 19.06.2019 inkl. LINQ

        public M GetSingle<P>(P pkValue)
        {
<<<<<<< HEAD
=======
            var entity = new M();
>>>>>>> 1b75c64bf9613056678978c0e6f12a0a5832905f
            try
            {
                using (var context = new DataContext(DbProvider, ConnectionString))
                {
<<<<<<< HEAD
                    var table = context.GetTable<M>();
                    IQueryable<M> tableQuery = table.Where(row => row.Id.Equals(pkValue));
                    return tableQuery.FirstOrDefault();
=======
                    cmd.CommandText = $"SELECT * FROM {this.TableName} WHERE {this.PrimaryKey} = @primaryKey;";
                    cmd.Parameters.AddWithValue("@primaryKey", pkValue);
                    using (var reader = cmd.ExecuteReader())
                    {
                            entity = GetEntityFromDB(reader);
                    }
>>>>>>> 1b75c64bf9613056678978c0e6f12a0a5832905f
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
                throw ex;
            }
        }// funktioniert, 19.06.2019 inkl. LINQ

        public virtual void Add(M entity)
        {
            try
            {
                using (var context = new DataContext(DbProvider, ConnectionString))
                {
<<<<<<< HEAD
                    var finish = context.Insert<M>(entity);
=======
                    cmd.CommandText = $"INSERT INTO {this.TableName}" +
                        $" ({InsertIntoEntityFieldForSqlStatement}) "  +
                        $"VALUES ({AddSqlStatementValues(entity)});";

                    var finish = cmd.ExecuteNonQuery();
>>>>>>> 1b75c64bf9613056678978c0e6f12a0a5832905f
                    if (finish == 1)
                    {
                        MessageBox.Show("Hinzufuegen war erfolgreich!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
                throw ex;
            }
        } // funktioniert, 19.06.2019 inkl. LINQ

        public virtual void Delete(M entity)
        {
            try
            {
                using (var context = new DataContext(DbProvider, ConnectionString))
                {
<<<<<<< HEAD
                    var finish = context.Delete<M>(entity);
=======
                    cmd.CommandText = $"DELETE FROM {this.TableName} WHERE {PrimaryKey} = {entity.Id}";
                    var finish = cmd.ExecuteNonQuery();
>>>>>>> 1b75c64bf9613056678978c0e6f12a0a5832905f
                    if (finish == 1)
                    {
                        MessageBox.Show("Loeschen war erfolgreich!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        } // funktioniert, 19.06.2019 inkl. LINQ

        public virtual void Update(M entity)
        {
            try
            {
                using (var context = new DataContext(DbProvider, ConnectionString))
                {
<<<<<<< HEAD
                    var finish = context.Update<M>(entity);
=======
                    cmd.CommandText = $"UPDATE {this.TableName} SET {UpdateSqlStatementValues(entity)}  WHERE {PrimaryKey} = {entity.Id};";
                    var finish = cmd.ExecuteNonQuery();
>>>>>>> 1b75c64bf9613056678978c0e6f12a0a5832905f
                    if (finish == 1)
                    {
                        MessageBox.Show("Update war erfolgreich!");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        } // funktioniert, 19.06.2019 inkl. LINQ

        public IQueryable<M> GetAll(Expression<Func<M, bool>> whereCondition)
        {
            try
            {
                using (var context = new DataContext(DbProvider, ConnectionString))
                {
                    return context.GetTable<M>().Where(whereCondition);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
                throw ex;
            }
        } // funktioniert, 21.06.2019 inkl. LINQ

        public IQueryable<M> GetAll()
        {
            try
            {
                using (var context = new DataContext(DbProvider, ConnectionString))
                {
                    return context.GetTable<M>();
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
               throw ex;
            }
        } // funktioniert, 19.06.2019 inkl. LINQ
    }
}
