using LinqToDB;
using MonitoringClient.Model;
using MonitoringClient.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
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
        public string DbProvider { get; }

        protected RepositoryBase()
        {
            this.ConnectionString = "Server = localhost; Database = inventarisierungsloesung; Uid = root; Pwd = password;"/*"Server = localhost; Database = ; Uid = root; Pwd = ;"*/;
            this.DbProvider = "MySql";
        }

        public long Count(Expression <Func<M, bool>> whereCondition)
        {
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
            try
            {
                using (var context = new DataContext(DbProvider, ConnectionString))
                {
                    var table = context.GetTable<M>();
                    IQueryable<M> tableQuery = table.Where(row => row.Id.Equals(pkValue));
                    return tableQuery.FirstOrDefault();
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
                    var finish = context.Insert<M>(entity);
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
                    var finish = context.Delete<M>(entity);
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
                    var finish = context.Update<M>(entity);
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
