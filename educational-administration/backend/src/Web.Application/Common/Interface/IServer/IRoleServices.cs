using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;

namespace Web.Application.Common.Interface.IServer
{
    public interface IRoleServices
    {
        //新增角色
        Task<string> AddRole(RoleDTO role);

        //删除角色（指定名称）
        Task<string> DeleteRole(Guid id);

        //获取角色列表或者（指定名称模糊查询）
        string GetListOrByRoleName(PageFromQuery query);

        //查询角色（根据id）
        Task<string> GetbyId(Guid id);

        //修改角色名称（指定id）
        Task<string> UpdatedRoleName(Guid id, RoleDTO role);
    }
}