using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data;
using System.Linq.Expressions;

namespace BW.Repository.Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        private BetterWorkEntities dataContext;
        private readonly IDbSet<T> dbset;
        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
           // dataContext = new BetterWorkEntities();
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<T>();
        }
        /// <summary>
        /// 
        /// </summary>
        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }
        protected BetterWorkEntities DataContext
        {
            get { return dataContext ?? (dataContext = new BetterWorkEntities()); }
        }
        /// <summary>
        /// Data Context
        /// </summary>
        //protected BWDataContext DataContext
        //{
        //    get { return dataContext ?? (dataContext = new BetterWorkEntities()); }
        //}
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            dbset.Attach(entity);
            dbset.Remove(entity);
        }
        /// <summary>
        /// Delete Lamda
        /// </summary>
        /// <param name="where"></param>
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
            dataContext.SaveChanges();
        }
        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetById(long id)
        {
            return dbset.Find(id);
        }
        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetById(string id)
        {
            return dbset.Find(id);
        }

        public virtual T GetById(int id)
        {
            return dbset.Find(id);
        }
        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            return dbset.AsEnumerable();
        }
        /// <summary>
        /// Get many
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).AsEnumerable();
        }
        /// <summary>
        /// Get Lamda
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T Get(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).FirstOrDefault<T>();
        }
    }
}
