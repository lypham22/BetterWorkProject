using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BW.Data.Contract.DTOs;
using BW.Repository.Data.Infrastructure;
using System.Data.SqlClient;
using System.Data;

namespace BW.Repository.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public List<User> GetAllUser()
        {
            var result = this.GetAll()
                          .Select(u => new User
                          {
                              UserId = u.UserId,
                              UserName = u.UserName,
                              Email = u.Email
                          }).ToList();
            return result;
        }

        /// <summary>
        /// TODO: Delete: Using for demo
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUserAndRole()
        {
            RoleRepository roleRepository = new RoleRepository(this.DatabaseFactory);
            var list = this.GetAll()
                .Join(roleRepository.GetAll(), left => left.UserId, right => right.RoleID,
                    (left, right) => new User
                    {
                        UserId = left.UserId,
                        UserName = right.RoleName
                    })
                    .OrderBy(p => p.UserName).ToList();
            return list;
        }

        public bool CreateUser(User user)
        {
            this.Add(user);
            this.DataContext.SaveChanges();
            return true;
        }

        public bool UpdateUser(User user)
        {
            var userData = this.GetById(user.UserId);
            if (userData != null)
            {
                userData.Email = user.Email;
                userData.UserName = user.UserName;
                userData.UpdatedDate = user.UpdatedDate;
                this.Update(userData);
                this.DataContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteUser(int userId)
        {
            var userData = this.GetById(userId);
            if (userData != null)
            {
                this.Delete(userData);
                this.DataContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        // Execute store procedure
        /// <summary>
        /// TODO: Delete: Using for demo
        /// </summary>
        /// <returns></returns>
        public List<User> SPGetAllUser()
        {
            var id = new SqlParameter
            {
                ParameterName = "ID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var result = DataContext.Database.SqlQuery<Role>("getalluser").ToList<Role>();
            return new List<User>();
        }
    }
}
