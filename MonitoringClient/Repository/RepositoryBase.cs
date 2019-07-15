using LinqToDB;
using MonitoringClient.Services;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

namespace MonitoringClient.Repository
{
    public abstract class RepositoryBase<M> : IRepositoryBase<M>
        where M : class, IModel, new()
    {
        public abstract string TableName { get; }
        public string ConnectionString { get; set; }
        public string DbProvider { get; }

        protected RepositoryBase()
        {
            this.ConnectionString = "Server = localhost; Database = ; Uid = root; Pwd = ;";
            this.DbProvider = "MySql";
        }
<<<<<<< HEAD

=======
>>>>>>> ad0ff86a7bd02819f6ed5bee2e50fdc4972d4286
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
        }

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
        }

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
        }

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
        }

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
        }

        public IQueryable<M> GetAll(Expression<Func<M, bool>> whereCondition)
        {
            using (var context = new DataContext(DbProvider, ConnectionString))
            {
                return context.GetTable<M>().Where(whereCondition);
            }
        } // funktioniert, 21.06.2019 inkl. LINQ

        public IQueryable<M> GetAll()
        {
            using (var context = new DataContext(DbProvider, ConnectionString))
            {
                return context.GetTable<M>();
            }
        }
    }
}
