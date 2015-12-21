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
    public class RoleInPermissonRepository : RepositoryBase<BW_RoleInPermission>, IRoleInPermissonRepository
    {
        public RoleInPermissonRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        private RoleRepository roleRepository;
        private ModuleRepository moduleRepository;
        public ResponeMessage<List<RoleInPermissonDTO>> GetAllRoleInPermission()
        {
            var response = new ResponeMessage<List<RoleInPermissonDTO>> { Code = ErrorCodeEnum.SUCCESS, Data = new List<RoleInPermissonDTO>() };
            var result = this.GetAll()
                          .Join(roleRepository.GetAll(), l => l.RoleId, r => r.RoleId, (l, r) => new { l, r })
                          .Join(moduleRepository.GetAll(), l1 => l1.l.ModuleId, r1 => r1.ModuleId, (l1, r1) => new { l1, r1 })
                          .Select(p => new RoleInPermissonDTO
                          {
                              RoleInPermissionId = p.l1.l.RoleInPermissionId,
                              ModuleId = p.l1.l.ModuleId,
                              RoleId = p.l1.l.RoleId,
                              PAdd = p.l1.l.PAdd,
                              PEdit = p.l1.l.PEdit,
                              PDelete = p.l1.l.PDelete,
                              PView = p.l1.l.PView,
                              CreatedDate = (DateTime) p.l1.l.CreatedDate,
                              //UpdatedDate = (DateTime) p.l1.l.UpdatedDate,
                              RoleName = p.l1.r.RoleName,
                              ModuleName = p.r1.ModuleName
                          }).ToList();
            response.Data = result;
            return response;
        }
        //public ResponeMessage<RoleDTO> GetRoleById(int roleId)
        //{
        //    var response = new ResponeMessage<RoleDTO> { Code = ErrorCodeEnum.SUCCESS, Data = new RoleDTO() };
        //    var result = this.GetById(roleId);
        //    if (result != null)
        //    {
        //        response.Data = this.ToRoleDTO(result);
        //        return response;
        //    }
        //    return response;
        //}

        //public ResponeMessageBaseType<bool> CreateRole(RoleCreateDTO role)
        //{
        //    var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
        //    BW_Role r = new BW_Role();
        //    r.RoleName = role.RoleName;
        //    r.RoleDescription = role.RoleDescription;
        //    r.CreatedDate = DateTime.Now;
        //    r.IsActive = true;
        //    this.Add(r);
        //    this.DataContext.SaveChanges();
        //    return response;
        //}
        //public ResponeMessageBaseType<bool> UpdateRole(RoleCreateDTO role)
        //{
        //    var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
        //    var roleData = this.GetById(role.RoleId);
        //    if (roleData != null)
        //    {
        //        roleData.RoleName = role.RoleName;
        //        roleData.RoleDescription = role.RoleDescription;
        //        roleData.UpdatedDate = DateTime.Now;
        //        roleData.IsActive = role.IsActive;
        //        this.Update(roleData);
        //        this.DataContext.SaveChanges();
        //        return response;
        //    }
        //    else
        //    {
        //        response.Data = false;
        //        return response;
        //    }
        //}

        //public ResponeMessageBaseType<bool> DeleteRole(int roleId)
        //{
        //    var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
        //    var roleData = this.GetById(roleId);
        //    if (roleData != null)
        //    {
        //        string delete = "DELETE FROM BW_UserInRole WHERE RoleId = " + roleId;
        //        DataContext.Database.ExecuteSqlCommand(delete);

        //        this.Delete(roleData);
        //        this.DataContext.SaveChanges();

        //        return response;
        //    }
        //    else
        //    {
        //        response.Data = false;
        //        return response;
        //    }

        //}
        //private RoleDTO ToRoleDTO(BW_Role obj)
        //{
        //    return new RoleDTO()
        //    {
        //        RoleId = obj.RoleId,
        //        RoleName = obj.RoleName,
        //        RoleDescription = obj.RoleDescription,
        //        CreatedDate = (DateTime)obj.CreatedDate,
        //        IsActive = obj.IsActive
        //    };
        //}
    }
}
