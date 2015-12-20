using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BW.Data.Contract.DTOs;
using BW.Repository.Data.Infrastructure;
using System.Data.SqlClient;
using System.Data;
using BW.Data.Contract;
using BW.Common.Enums;

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
        public ResponeMessage<List<UserDTO>> GetAllUser()
        {
            var response = new ResponeMessage<List<UserDTO>> { Code = ErrorCodeEnum.SUCCESS, Data = new List<UserDTO>() };
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
            StringBuilder builder;
            foreach (var item in result)
            {
                roles = userInRole.GetMany(r => r.UserId == item.UserId && r.BW_Role.IsActive == true).Select(u => new RoleDTO
                {
                    RoleName = u.BW_Role.RoleName
                }).ToList();
                builder = new StringBuilder();
                foreach (var r in roles)
                {
                    builder.Append(r.RoleName).Append(", ");
                }

                item.RoleName = !string.IsNullOrEmpty(builder.ToString()) ? builder.Remove(builder.Length - 2, 1).ToString() : string.Empty;
            }
            response.Data = result;
            response.Message = "GetAllUser Success";
            return response;
        }
        public ResponeMessage<UserDTO> GetUserById(int userId)
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
        public ResponeMessageBaseType<bool> CreateUser(UserCreateDTO user)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = false };
            BW_User u = new BW_User();
            u.FirstName = user.FirstName;
            u.LastName = user.LastName;
            u.Email = user.Email;
            u.Password = user.Password;
            u.CreatedDate = DateTime.Now;
            u.IsActive = true;
            this.Add(u);
            this.DataContext.SaveChanges();
           
            foreach (var item in user.roles)
            {
                string sql = @"INSERT INTO BW_UserInRole(UserId, RoleId, CreatedDate) VALUES({0}, {1}, {2})";
                DataContext.Database.ExecuteSqlCommand(sql, u.UserId, int.Parse(item.ToString()), DateTime.Now);
            }
            response.Data = true;
            return response;
        }
        public ResponeMessageBaseType<bool> UpdateUser(BW_User user)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = false };
            var userData = this.GetById(user.UserId);
            if (userData != null)
            {
                userData.Email = user.Email;
                //userData.UpdatedDate = user.UpdatedDate;
                this.Update(userData);
                this.DataContext.SaveChanges();
                response.Data = true;
            }
            else
            {
                response.Data = false;
            }
            return response;
        }
        public ResponeMessageBaseType<bool> DeleteUser(int userId)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = false };
            var userData = this.GetById(userId);
            if (userData != null)
            {
                this.Delete(userData);
                this.DataContext.SaveChanges();
                response.Data = true;
            }
            else
            {
                response.Data = false;
            }

            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ResponeMessage<AuthenticationInfoDTO> Login(string email, string password)
        {
            var response = new ResponeMessage<AuthenticationInfoDTO> { Code = ErrorCodeEnum.SUCCESS, Data = new AuthenticationInfoDTO() };
            var authen = this.GetMany(u => u.Email == email && u.Password == password).Select(u => new AuthenticationInfoDTO { 
                UserId = u.UserId,
                Email = u.Email,
                Password = u.Password,
                FirstName = u.FirstName,
                LastName = u.LastName,
                IsActive = u.IsActive,
                CreatedDate = u.CreatedDate
            }).FirstOrDefault();
            if (authen != null) {
                var userId = new SqlParameter
                {
                    ParameterName = "UserId",
                    Value = authen.UserId
                };
                var result = DataContext.Database.SqlQuery<ModuleDTO>("spa_BW_GetPermissionListByUserId  @UserId", userId).ToList<ModuleDTO>();
                List<ModuleDTO> permission = new List<ModuleDTO>();
                ModuleDTO data = null;
                string permissionName = string.Empty;
                foreach (var item in result)
                {
                    if (string.IsNullOrEmpty(item.Permission)) continue;
                    var str = item.Permission.Remove(item.Permission.Length - 1, 1).Split(',');
                    foreach (var per in str)
                    {
                        if (!string.IsNullOrEmpty(item.Permission))
                        { 
                            data = new ModuleDTO();
                            data.ModuleId = item.ModuleId;
                            data.ModuleName = item.ModuleName;
                            permissionName = data.ModuleName + per;
                            data.Permission = permissionName;
                            if (permission.Find(x => x.Permission.Contains(permissionName)) != null) continue;
                            permission.Add(data);
                        }
                    }
                }
            }
            return response;
        }
    }
}
