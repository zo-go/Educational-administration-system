using Microsoft.AspNetCore.Mvc;
using Web.Application.Common.Interface;
using Web.Application.Common.Interface.IServer;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;
using Web.Application.ResDto;
using Web.Application.Utils;
using Web.Domain.Entity;
using Web.Services.Services;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // 请求地址 http://localhost:5003/user
    public class UserController : ControllerBase
    {

        private readonly IUserServices _users;
        private readonly IRepository<AppUser> _user;
        private readonly IRepository<AppRole> _role;
        public UserController(IUserServices users,IRepository<AppUser> user,IRepository<AppRole> role)
        {
            _users = users;
            _user = user;
            _role = role;
        }

        // get
        public string getUser([FromQuery] PageFromQuery query)
        {
            var tmp = _users.GetListOrByUserName(query);

            return tmp;
        }

        // get
        [Route("admin")]
        public string getAdminUser([FromQuery] PageFromQuery query)
        {
            var roleid = _role.Table.Where(x=>x.RoleName == "管理员").FirstOrDefault()!.Id;
            var list = _user.Table.Where(x=>x.RoleId == roleid).ToList();
            return new
                {
                    Code = 200,
                    Msg = "获取管理员列表成功",
                    Data = list
                }.SerializeObject();
        }

        // post
        [HttpPost]
        public async Task<string> addUser(UserDTO userDto)
        {
            var tmp = await _users.AddUser(userDto);

            return tmp;
        }

        // put
        [HttpPut("{id}")]
        public async Task<string> updateUser(UserDTO userDto, Guid id)
        {
            var tmp = await _users.UpdateUser(id, userDto);

            return tmp;
        }

        // delete
        [HttpDelete("{id}")]
        public async Task<string> deleteUser(Guid id)
        {
            var tmp = await _users.DeleteUser(id);

            return tmp;
        }

        //根据账号密码获取openid
        [HttpGet("{id}")]
        public string GetOpenId(string id)
        {
            var tmp = _users.GetOpenId(id);

            return tmp;
        }
        //根据账号密码获取openid
        [HttpPost("AddOpenId")]
        public async Task<string> AddOpenId(UserOpenIdDTO openIdDTO)
        {
            var tmp = await _users.AddOpenid(openIdDTO);

            return tmp;
        }
        [HttpPost("GetByStudentInfo")]
        public string GetByStudentInfo(UserForAuth user)
        {
            var tmp = _users.GetByStudentInfo(user);

            return tmp;
        }
    }
}