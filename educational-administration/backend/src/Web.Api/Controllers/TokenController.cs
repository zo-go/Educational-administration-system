using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Common.Interface;
using Web.Application.ReqDto;
using Web.Application.ResDto;
using Web.Domain.Entity;
using Web.Application.Utils;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IRepository<AppUser> _userRepository;
        private readonly IRepository<AppRole> _roleRepository;
        private readonly IIdentityService _identityService;
        public TokenController(IRepository<AppUser> userRepository, IRepository<AppRole> roleRepository, IIdentityService identityService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _identityService = identityService;

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/login")]
        public async Task<string> Token(UserForAuth userForAuth)
        {
            //验证 该用户是否存在  false则401 true将生成Token
            if (!_identityService.ValidateUserAsync(userForAuth))
            {
                return new { code = 402, msg = "用户不存在" }.SerializeObject();
            }
            var user = _userRepository.Table.Where(x => x.UserName == userForAuth.Username && x.PassWord == userForAuth.Password);

            var role = _roleRepository.Table.Where(x => x.IsDeleted == false);

            var result = user.Join(role, x => x.RoleId, p => p.Id, (x, y) => new
            {
                userInfo = x,
                roleInfo = y
            });

            return new { code = 200, msg = "登入成功", data = result, Token = await _identityService.GenerateTokenAsync() }.SerializeObject();


        }
        [AllowAnonymous]
        [HttpPost("/user/refreshtoken")]
        public async Task<string> refreshtoken(AppToken appToken)
        {
            var tokenToReturn = await _identityService.RefreshTokenAsync(appToken);
            System.Console.WriteLine("刷新后的Token===============================" + appToken.AccessToken);
            if (tokenToReturn != null)
            {
                var res = new
                {
                    Code = 1000,
                    Msg = "刷新token成功",
                    Data = tokenToReturn
                };
                return res.SerializeObject();
            }
            else
            {
                var res = new
                {
                    Code = 401,
                    Msg = "token或refreshToken无效，请重新登录",
                    Data = ""
                };
                return res.SerializeObject();
            }
        }
    }
}