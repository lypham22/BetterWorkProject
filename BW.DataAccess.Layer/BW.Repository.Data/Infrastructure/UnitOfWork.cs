using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BW.Repository.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory databaseFactory;
        private BWDataContext dataContext;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseFactory"></param>
        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }
        /// <summary>
        /// 
        /// </summary>
        protected BWDataContext DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.Get()); }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Commit()
        {
            DataContext.Commit();
        }
    }
}
