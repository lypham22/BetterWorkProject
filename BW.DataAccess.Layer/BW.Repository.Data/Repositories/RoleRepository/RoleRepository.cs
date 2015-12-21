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
    public class RoleRepository : RepositoryBase<BW_Role>, IRoleRepository
    {
        public RoleRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public ResponeMessage<List<RoleDTO>> GetAllRole()
        {
            var response = new ResponeMessage<List<RoleDTO>> { Code = ErrorCodeEnum.SUCCESS, Data = new List<RoleDTO>() };
            var result = this.GetAll()
                          .Select(u => new RoleDTO
                          {
                              RoleId = u.RoleId,
                              RoleName = u.RoleName,
                              RoleDescription = u.RoleDescription,
                              IsActive = u.IsActive,
                              CreatedDate = (DateTime)u.CreatedDate
                          }).ToList();
            response.Data = result;
            return response;
        }
        public ResponeMessage<RoleDTO> GetRoleById(int roleId)
        {
            var response = new ResponeMessage<RoleDTO> { Code = ErrorCodeEnum.SUCCESS, Data = new RoleDTO() };
            var result = this.GetById(roleId);
            if (result != null)
            {
                response.Data = this.ToRoleDTO(result);
                return response;
            }
            return response;
        }

        public ResponeMessageBaseType<bool> CreateRole(RoleCreateDTO role)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
            BW_Role r = new BW_Role();
            r.RoleName = role.RoleName;
            r.RoleDescription = role.RoleDescription;
            r.CreatedDate = DateTime.Now;
            r.IsActive = true;
            this.Add(r);
            this.DataContext.SaveChanges();
            return response;
        }
        public ResponeMessageBaseType<bool> UpdateRole(RoleCreateDTO role)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
            var roleData = this.GetById(role.RoleId);
            if (roleData != null)
            {
                roleData.RoleName = role.RoleName;
                roleData.RoleDescription = role.RoleDescription;
                roleData.UpdatedDate = DateTime.Now;
                roleData.IsActive = role.IsActive;
                this.Update(roleData);
                this.DataContext.SaveChanges();
                return response;
            }
            else
            {
                response.Data = false;
                return response;
            }
        }

        public ResponeMessageBaseType<bool> DeleteRole(int roleId)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
            var roleData = this.GetById(roleId);
            if (roleData != null)
            {
                string delete = "DELETE FROM BW_UserInRole WHERE RoleId = " + roleId;
                DataContext.Database.ExecuteSqlCommand(delete);

                this.Delete(roleData);
                this.DataContext.SaveChanges();

                return response;
            }
            else
            {
                response.Data = false;
                return response;
            }

        }
        private RoleDTO ToRoleDTO(BW_Role obj)
        {
            return new RoleDTO()
            {
                RoleId = obj.RoleId,
                RoleName = obj.RoleName,
                RoleDescription = obj.RoleDescription,
                CreatedDate = (DateTime)obj.CreatedDate,
                IsActive = obj.IsActive
            };
        }
    }
}
