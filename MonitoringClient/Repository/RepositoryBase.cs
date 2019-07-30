using MonitoringClient.Model;
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
            this.ConnectionString = "InventarisierungsloesungEntities";/*@"Data Source=.\; initial catalog=Inventarisierungsloesung;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework;";*/

        }

        public long Count(Expression<Func<TEntity, bool>> whereCondition)
        {
            try
            {
                var context = new InventarisierungsloesungEntities();

                return context.Set<TEntity>().Where(whereCondition).Count();

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
                var context = new InventarisierungsloesungEntities();

                return context.Set<TEntity>().Count();


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
                var context = new InventarisierungsloesungEntities();
                return context.Set<TEntity>().Find(pkValue);
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
                var context = new InventarisierungsloesungEntities();
                context.Set<TEntity>().Add(entity);
                int finish = context.SaveChanges();
                if (finish > 0)
                {
                    MessageBox.Show("Hinzufuegen war erfolgreich!");
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
                var context = new InventarisierungsloesungEntities();

                if (entity != null)
                {
                    TEntity deletedEntity = context.Set<TEntity>().Where(c => c.Id == entity.Id).FirstOrDefault();
                    context.Set<TEntity>().Remove(deletedEntity);
                    int finish = context.SaveChanges();
                    if (finish > 0)
                    {
                        MessageBox.Show("Löschen war erfolgreich!");
                    }
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
                var context = new InventarisierungsloesungEntities();

                var foundedEntity = GetSingle(entity.Id);
                if (foundedEntity != null)
                {
                    //Update
                    foundedEntity = entity;
                    context.Entry(foundedEntity).State = EntityState.Modified;
                    int finish = context.SaveChanges();
                    if (finish > 0)
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

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> whereCondition)
        {
            var context = new InventarisierungsloesungEntities();

            return context.Set<TEntity>().Where(whereCondition);


        }

        public IQueryable<TEntity> GetAll()
        {
            var context = new InventarisierungsloesungEntities();
            return context.Set<TEntity>();
        }
    }
}
