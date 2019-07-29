
using MonitoringClient.Repository.Context;
using MonitoringClient.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

namespace MonitoringClient.Repository
{
    public abstract class RepositoryBase<TEntity> : DbContext, IRepositoryBase<TEntity>
        where TEntity : class, IModel, new()
    {
        public string ConnectionString { get; set; } // "Server = localhost; Database = inventarisierungsloesung; Uid = root; Pwd = password;"

        protected RepositoryBase()
        {
            this.ConnectionString = "InventarisierungsloesungDB";/*@"Data Source=.\; initial catalog=Inventarisierungsloesung;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework;";*/

        }

        public long Count(Expression<Func<TEntity, bool>> whereCondition)
        {
            try
            {
                using (var context = new InventarisierungsloesungDB(ConnectionString))
                {
                    return context.Set<TEntity>().Where(whereCondition).Count();
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
                using (var context = new InventarisierungsloesungDB(ConnectionString))
                {
                    return context.Set<TEntity>().Count();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
                throw ex;
            }
        }

        public TEntity GetSingle<P>(P pkValue)
        {
            try
            {
                using (var context = new InventarisierungsloesungDB(ConnectionString))
                {
                    var table = context.Set<TEntity>();
                    IQueryable<TEntity> tableQuery = table.Where(row => row.Id.Equals(pkValue));
                    return tableQuery.FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
                throw ex;
            }
        }

        public virtual void Add(TEntity entity)
        {
            try
            {
                using (var context = new InventarisierungsloesungDB(ConnectionString))
                {
                    context.Set<TEntity>().Add(entity);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
                throw ex;
            }
        }

        public virtual void Delete(TEntity entity)
        {
            try
            {
                using (var context = new InventarisierungsloesungDB(ConnectionString))
                {
                    context.Set<TEntity>().Remove(entity);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        }

        public virtual void Update(TEntity entity)
        {
            try
            {
                using (var context = new InventarisierungsloesungDB(ConnectionString))
                {
                    var result = GetSingle(entity.Id);
                    if (result != null)
                    {
                        result = entity;
                        int finish = context.SaveChanges();
                        if (finish > 0)
                        {
                            MessageBox.Show("Hinzufuegen war erfolgreich!");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> whereCondition)
        {
            using (var context = new InventarisierungsloesungDB(ConnectionString))
            {
                return context.Set<TEntity>().Where(whereCondition);
            }

        }

        public IEnumerable<TEntity> GetAll()
        {
            IEnumerable<TEntity> collection;
            using (var context = new InventarisierungsloesungDB(ConnectionString))
            {
                return collection = context.Set<TEntity>().AsNoTracking();

            }

        }
    }
}
