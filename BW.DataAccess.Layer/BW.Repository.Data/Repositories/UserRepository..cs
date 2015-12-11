using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BW.Data.Contract.DTOs;
using BW.Repository.Data.Infrastructure;

namespace BW.Repository.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public void Save()
        {
            var data = this.GetAll().Select(visit => new User
            {
                Name = visit.Name
            }).ToList();
            this.DataContext.Commit();
        }
    }
    public interface IUserRepository : IRepository<User>
    {
        void Save();
    }   
}
