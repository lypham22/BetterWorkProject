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
                roles = userInRole.GetMany(r => r.UserId == item.UserId && r.BW_Role.IsActive == true).Select(u => new RoleDTO { 
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

        //public UserDetailsDTO GetUserById(int userId)
        //{
        //    userInRole = new UserInRoleRepository(this.DatabaseFactory);
        //    UserDetailsDTO result = new UserDetailsDTO();

        //    var roles = userInRole.GetMany(u => u.UserId == userId).Select(u => new RoleDTO
        //    {
        //        RoleID = u.RoleId,
        //        RoleName = u.BW_Role.RoleName
        //    }).ToList();
            
        //    var user = this.GetMany(u => u.UserId == userId).Select(u => new UserDTO
        //    {
        //        UserId = u.UserId,
        //        FirstName = u.FirstName,
        //        LastName = u.LastName,
        //        Email = u.Email,
        //        CreatedDate = u.CreatedDate,
        //        IsActive = u.IsActive,
        //        RoleName = string.Join(",", roles)
        //    }).FirstOrDefault();

        //    result.user = user;
        //    //result.roles = roles;
        //    return result;
        //}

        /// <summary>
        /// TODO: Delete: Using for demo
        /// </summary>
        /// <returns></returns>
        public List<BW_UserInRole> GetAllUserAndRole()
        {
            RoleRepository roleRepository = new RoleRepository(this.DatabaseFactory);
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

            BW_UserInRole uir = new BW_UserInRole();
            userInRole = new UserInRoleRepository(this.DatabaseFactory);
            int lenghtR = user.RoleId.Count;
            for (int i = 0; i < lenghtR; i++)
            {
                uir.UserId = user.UserId;
                uir.RoleId = user.RoleId[i];
                uir.CreatedDate = DateTime.Now;
                userInRole.Add(uir);
            }
            
            //this.Add(user);

            //int userId = user.UserId;
            //List<int> roleId = user.;
            //userInRole = new UserInRoleRepository(DatabaseFactory);
            this.DataContext.SaveChanges();
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
