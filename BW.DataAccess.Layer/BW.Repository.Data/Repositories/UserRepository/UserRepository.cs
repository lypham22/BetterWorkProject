using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BW.Data.Contract.DTOs;
using BW.Repository.Data.Infrastructure;
using System.Data.SqlClient;
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
            var response = new ResponeMessage<UserDTO> { Code = ErrorCodeEnum.SUCCESS, Data = new UserDTO() };
            var result = this.GetById(userId);
            if (result != null)
            {
                UserDTO userDTO = this.ToUserDTO(result);
                userInRole = new UserInRoleRepository(this.DatabaseFactory);
                List<RoleDTO> roles = userInRole.GetMany(r => r.UserId == userId && r.BW_Role.IsActive == true).Select(u => new RoleDTO
                {
                    RoleName = u.BW_Role.RoleName,
                    RoleId = u.BW_Role.RoleId,
                    RoleDescription = u.BW_Role.RoleDescription,
                    CreatedDate = (DateTime)u.BW_Role.CreatedDate,
                    IsActive = u.BW_Role.IsActive
                }).ToList();

                response.Data = userDTO;
                response.Data.RoleDTOs = roles;
                response.Data.RoleName = string.Join(", ", userDTO.RoleDTOs.Select(x => x.RoleName).ToArray());
                response.Message = "GetUserById Success";
                return response;
            }
            return response;
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

        public ResponeMessageBaseType<bool> UpdateUser(UserCreateDTO user)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = false };
            var userData = this.GetById(user.UserId);
            if (userData != null)
            {
                userData.FirstName = user.FirstName;
                userData.LastName = user.LastName;
                userData.UpdatedDate = DateTime.Now;
                userData.IsActive = user.IsActive;
                this.Update(userData);
                this.DataContext.SaveChanges();

                string delete = "DELETE FROM BW_UserInRole WHERE UserId = " + userData.UserId;
                DataContext.Database.ExecuteSqlCommand(delete);
                foreach (var item in user.roles)
                {
                    string update = "INSERT INTO BW_UserInRole(UserId, RoleId, CreatedDate) VALUES(" + userData.UserId + "," + int.Parse(item.ToString()) + ", '" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") + "')";
                    DataContext.Database.ExecuteSqlCommand(update);
                }
                response.Data = true;
                return response;
            }
            return response;
        }

        public ResponeMessageBaseType<bool> UpdateProfile(UserProfileDTO user)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = false };
            var userData = this.GetById(user.UserId);
            if (userData != null)
            {
                userData.FirstName = user.FirstName;
                userData.LastName = user.LastName;
                userData.UpdatedDate = DateTime.Now;
                this.Update(userData);
                this.DataContext.SaveChanges();

                response.Data = true;
                return response;
            }
            return response;
        }

        public ResponeMessageBaseType<bool> DeleteUser(int userId)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = false };
            var userData = this.GetById(userId);
            if (userData != null)
            {
                string delete = "DELETE FROM BW_UserInRole WHERE UserId = " + userId;
                DataContext.Database.ExecuteSqlCommand(delete);

                this.Delete(userData);
                this.DataContext.SaveChanges();
                response.Data = true;
                return response;
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
            var response = new ResponeMessage<AuthenticationInfoDTO> { Code = ErrorCodeEnum.SUCCESS};
            var authen = this.GetMany(u => u.Email == email && u.Password == password).Select(u => new AuthenticationInfoDTO
            {
                UserId = u.UserId,
                Email = u.Email,
                Password = u.Password,
                FirstName = u.FirstName,
                LastName = u.LastName,
                IsActive = u.IsActive,
                CreatedDate = u.CreatedDate
            }).FirstOrDefault();
            if (authen != null)
            {
                response.Data = new AuthenticationInfoDTO();
                response.Data = authen;
                var userId = new SqlParameter
                {
                    ParameterName = "UserId",
                    Value = authen.UserId
                };
                var result = DataContext.Database.SqlQuery<ModuleDTO>("spa_BW_GetPermissionListByUserId  @UserId", userId).ToList<ModuleDTO>();
                List<ModuleDTO> permissions = new List<ModuleDTO>();
                ModuleDTO data = null;
                string permissionName = string.Empty;
                foreach (var item in result)
                {
                    if (string.IsNullOrEmpty(item.PermissionCode)) continue;
                    var str = item.PermissionCode.Remove(item.PermissionCode.Length - 1, 1).Split(',');
                    foreach (var per in str)
                    {
                        if (!string.IsNullOrEmpty(item.PermissionCode))
                        {
                            data = new ModuleDTO();
                            data.ModuleId = item.ModuleId;
                            data.ModuleName = item.ModuleName;
                            permissionName = per + item.ModuleCode;
                            data.PermissionCode = permissionName;
                            if (permissions.Find(x => x.PermissionCode.Contains(permissionName)) != null) continue;
                            permissions.Add(data);
                        }
                    }
                }
                response.Data.modules = permissions;
            }
            return response;
        }
        public ResponeMessage<AuthenticationInfoDTO> AutoUpdatePermForUser(string email)
        {
            var response = new ResponeMessage<AuthenticationInfoDTO> { Code = ErrorCodeEnum.SUCCESS, Data = new AuthenticationInfoDTO() };
            var authen = this.GetMany(u => u.Email == email).Select(u => new AuthenticationInfoDTO
            {
                UserId = u.UserId,
                Email = u.Email,
                Password = u.Password,
                FirstName = u.FirstName,
                LastName = u.LastName,
                IsActive = u.IsActive,
                CreatedDate = u.CreatedDate
            }).FirstOrDefault();
            if (authen != null)
            {
                response.Data = authen;
                var userId = new SqlParameter
                {
                    ParameterName = "UserId",
                    Value = authen.UserId
                };
                var result = DataContext.Database.SqlQuery<ModuleDTO>("spa_BW_GetPermissionListByUserId  @UserId", userId).ToList<ModuleDTO>();
                List<ModuleDTO> permissions = new List<ModuleDTO>();
                ModuleDTO data = null;
                string permissionName = string.Empty;
                foreach (var item in result)
                {
                    if (string.IsNullOrEmpty(item.PermissionCode)) continue;
                    var str = item.PermissionCode.Remove(item.PermissionCode.Length - 1, 1).Split(',');
                    foreach (var per in str)
                    {
                        if (!string.IsNullOrEmpty(item.PermissionCode))
                        {
                            data = new ModuleDTO();
                            data.ModuleId = item.ModuleId;
                            data.ModuleName = item.ModuleName;
                            permissionName = per + item.ModuleCode;
                            data.PermissionCode = permissionName;
                            if (permissions.Find(x => x.PermissionCode.Contains(permissionName)) != null) continue;
                            permissions.Add(data);
                        }
                    }
                }
                response.Data.modules = permissions;
            }
            return response;
        }
        private UserDTO ToUserDTO(BW_User obj)
        {
            return new UserDTO()
            {
                UserId = obj.UserId,
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                Email = obj.Email,
                Password = obj.Password,
                CreatedDate = obj.CreatedDate,
                IsActive = obj.IsActive
            };
        }

        public ResponeMessageBaseType<bool> UpdatePassword(UserDTO user)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = false };
            var userData = this.GetById(user.UserId);
            if (userData != null)
            {
                userData.Password = user.Password;
                this.Update(userData);
                this.DataContext.SaveChanges();
            }
            return response;
        }

        public ResponeMessageBaseType<bool> CheckUnitEmail(string email)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = false };
            var data = this.Get(x => x.Email == email);
            if (data != null)
            {
                response.Data = true;
                return response;
            }
            else
            {
                return response;
            }
        }
    }
}
