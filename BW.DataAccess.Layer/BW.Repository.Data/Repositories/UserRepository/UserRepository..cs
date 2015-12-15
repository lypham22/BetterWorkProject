﻿using System;
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
                              Id = u.Id,
                              Name = u.Name
                          }).ToList();
            return result;
        }

        public void CreateUser(User user)
        {
            this.Add(user);
            this.DataContext.Commit();
        }

        public bool UpdateUser(User user)
        {
            var userData = this.GetById(user.Id);
            if (userData != null)
            {
                userData.Name = user.Name;
                this.Update(userData);
                this.DataContext.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteUser(User user)
        {
            var userData = this.GetById(user.Id);
            if (userData != null)
            {
                this.Delete(userData);
                this.DataContext.Commit();
                return true;
            }
            else
            {
                return false;
            }

        }

        // Execute store procedure
        public List<User> SPGetAllUser()
        {
            var id = new SqlParameter
            {
                ParameterName = "ID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var result = DataContext.Database.SqlQuery<User>("store-pro-name @ID OUT", id).ToList<User>();
            return result;
        }
    }
}