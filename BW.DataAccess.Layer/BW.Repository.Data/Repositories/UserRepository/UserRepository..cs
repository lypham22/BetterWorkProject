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
    public class UserRepository : RepositoryBase<BW_User>, IUserRepository
    {
        public UserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        private UserInRoleRepository userInRole;
        private RoleRepository roleRepository;

        /// <summary>
        /// Get all user
        /// </summary>
        /// <returns></returns>
        public List<UserDTO> GetAllUser()
        {
            var result = this.GetAll()
                          .Select(u => new UserDTO
                          {
                              UserId = u.UserId,
                              FirstName = u.FirstName,
                              LastName = u.LastName,
                              Email = u.Email,
                              CreatedDate = u.CreatedDate,
                              IsActive = u.IsActive,
                          }).ToList();

            userInRole = new UserInRoleRepository(this.DatabaseFactory);
            List<RoleDTO> roles;
            foreach (var item in result)
            {
                roles = userInRole.GetMany(r => r.UserId == item.UserId && r.BW_Role.IsActive == true).Select(u => new RoleDTO
                {
                    RoleName = u.BW_Role.RoleName
                }).ToList();
                StringBuilder builder = new StringBuilder();
                foreach (var r in roles)
                {
                    builder.Append(r.RoleName).Append(", ");
                }

                item.RoleName = !string.IsNullOrEmpty(builder.ToString()) ? builder.Remove(builder.Length - 2, 1).ToString() : string.Empty;
            }

            return result;
        }

        public UserDTO GetUserById(int userId)
        {
            //var result = this.GetById(userId);
            //userInRole = new UserInRoleRepository(this.DatabaseFactory);
            //List<RoleDTO> roles = userInRole.GetMany(r => r.UserId == userId && r.BW_Role.IsActive == true).Select(u => new RoleDTO
            //{
            //    RoleName = u.BW_Role.RoleName
            //}).ToList();
            //StringBuilder builder = new StringBuilder();
            //foreach (var r in roles)
            //{
            //    builder.Append(r.RoleName).Append(", ");
            //}
            return null;   

            //item.RoleName = !string.IsNullOrEmpty(builder.ToString()) ? builder.Remove(builder.Length - 2, 1).ToString() : string.Empty;

            //var result = this.GetAll()
            //              .Select(u => new UserDTO
            //              {
            //                  UserId = u.UserId,
            //                  FirstName = u.FirstName,
            //                  LastName = u.LastName,
            //                  Email = u.Email,
            //                  CreatedDate = u.CreatedDate,
            //                  IsActive = u.IsActive,
            //              }).ToList();

            //userInRole = new UserInRoleRepository(this.DatabaseFactory);
            //List<RoleDTO> roles;
            //foreach (var item in result)
            //{
            //    roles = userInRole.GetMany(r => r.UserId == item.UserId && r.BW_Role.IsActive == true).Select(u => new RoleDTO
            //    {
            //        RoleName = u.BW_Role.RoleName
            //    }).ToList();
            //    StringBuilder builder = new StringBuilder();
            //    foreach (var r in roles)
            //    {
            //        builder.Append(r.RoleName).Append(", ");
            //    }

            //    item.RoleName = !string.IsNullOrEmpty(builder.ToString()) ? builder.Remove(builder.Length - 2, 1).ToString() : string.Empty;
            //}

            //return result;
        }
        /// <summary>
        /// TODO: Delete: Using for demo
        /// </summary>
        /// <returns></returns>
        public List<BW_UserInRole> GetAllUserAndRole()
        {
            roleRepository = new RoleRepository(this.DatabaseFactory);
            var list = this.GetAll()
                .Join(roleRepository.GetAll(), left => left.UserId, right => right.RoleId,
                    (left, right) => new BW_UserInRole
                    {
                        RoleId = right.RoleId,
                        //RoleName = right.RoleName,
                        UserId = left.UserId
                    })
                    .OrderBy(p => p.UserId).ToList();
            return list;
        }

        public bool CreateUser(UserCreateDTO user)
        {
            BW_User u = new BW_User();
            u.FirstName = user.FirstName;
            u.LastName = user.LastName;
            u.Email = user.Email;
            u.Password = user.Password;
            u.CreatedDate = DateTime.Now;
            u.IsActive = true;
            this.Add(u);
            this.DataContext.SaveChanges();
           
            var result = DataContext.Database.SqlQuery<BW_Role>("SELECT * FROM BW_ROLE").ToList<BW_Role>();
            BW_UserInRole uir = new BW_UserInRole();
            userInRole = new UserInRoleRepository(this.DatabaseFactory);
            RoleRepository roleRepository = new RoleRepository(this.DatabaseFactory);
            foreach (var item in user.roles)
            {
                string insert = "INSERT INTO BW_UserInRole(UserId, RoleId, CreatedDate) VALUES(" +u.UserId+ ","+ int.Parse(item.ToString()) + ", '" +  DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") + "')";
                DataContext.Database.ExecuteSqlCommand(insert);
                //var roleId = int.Parse(item.ToString());
                //uir.BW_Role = roleRepository.GetMany(r => r.RoleId == roleId).FirstOrDefault();
                //uir.BW_User = this.GetMany(us => us.UserId == u.UserId).FirstOrDefault();
                //uir.UserId = u.UserId;
                //uir.RoleId = roleId;
                //uir.CreatedDate = DateTime.Now;
                //userInRole.AddUserInRole(uir);
            }
            return true;
        }

        public bool UpdateUser(BW_User user)
        {
            var userData = this.GetById(user.UserId);
            if (userData != null)
            {
                userData.Email = user.Email;
                //userData.UpdatedDate = user.UpdatedDate;
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
        public List<BW_User> SPGetAllUser()
        {
            var id = new SqlParameter
            {
                ParameterName = "ID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var result = DataContext.Database.SqlQuery<BW_Role>("getalluser").ToList<BW_Role>();
            return new List<BW_User>();
        }
    }
}
