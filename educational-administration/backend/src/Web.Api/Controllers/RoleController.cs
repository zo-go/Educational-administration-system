using Microsoft.AspNetCore.Mvc;
using Web.Application.Common.Interface.IServer;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;
using Web.Application.ResDto;
using Web.Application.Utils;
using Web.Services.Services;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleServices _roles;

        public RoleController(IRoleServices roles)
        {
            _roles = roles;
        }

        // 获取角色列表 或 根据关键字筛选出符合条件的角色
        [HttpGet]
        public string GetRoleFun([FromQuery] PageFromQuery query)
        {
            var tmp = _roles.GetListOrByRoleName(query);

            return tmp;
        }

        // 添加角色
        [HttpPost]
        public async Task<string> AddRoleFun(RoleDTO role)
        {
            var res = await _roles.AddRole(role);

            return res;
        }

        // 修改角色
        [HttpPut("{id}")]
        public async Task<string> UpdateRoleFun(Guid id, RoleDTO role)
        {
            var res = await _roles.UpdatedRoleName(id, role);

            return res;
        }

        // 删除角色
        [HttpDelete("{id}")]
        public async Task<string> DeleteRoleFun(Guid id)
        {
            var res = await _roles.DeleteRole(id);

            return res;
        }
    }
}