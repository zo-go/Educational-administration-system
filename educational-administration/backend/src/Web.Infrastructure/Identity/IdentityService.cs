using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Web.Application.Common.Interface;
using Web.Application.Configuration;
using Web.Application.ReqDto;
using Web.Application.ResDto;
using Web.Domain.Entity;

namespace Web.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {

        private readonly IConfiguration _configuration;
        private readonly IRepository<AppUser> _appUserRepository;
        private readonly IRepository<AppIdentityUser> _appIdentityUserRepository;

        private AppUser? _appUser;

        public IdentityService(IConfiguration configuration, IRepository<AppUser> appUserRepository, IRepository<AppIdentityUser> appIdentityUserRepository)
        {
            _configuration = configuration;
            _appUserRepository = appUserRepository;
            _appIdentityUserRepository = appIdentityUserRepository;

        }
        public async Task<AppToken> GenerateTokenAsync()
        {
            //将配置实例绑定到类型 属性名称和键匹配进行绑定
            var jwtSetting = _configuration.GetSection("JwtSetting").Get<JwtSetting>();
            var claims = new[]
           {
                new Claim("UserId",_appUser!.Id.ToString()),
                new Claim("UserName",_appUser!.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(jwtSetting.Issuer, jwtSetting.Audience, claims, expires: DateTime.Now.AddMinutes(jwtSetting.AccessExpiration), signingCredentials: credentials);

            //生成token
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            //调用私有方法 生成 refreshToken
            var refreshToken = GenerateRefreshToken();


            var tmpIdentityUser = new AppIdentityUser
            {
                UserId = _appUser.Id,
                Username = _appUser.UserName,
                RefreshToken = refreshToken,
                RefreshTokenExpiration = DateTime.Now.AddMinutes(jwtSetting.RefreshExpiration)
            };
            // 根据用户ID 查找该登录用户的Token记录  并硬删除,再更新
            var identityUsers = _appIdentityUserRepository.Table.Where(x => x.UserId == _appUser.Id).FirstOrDefault();
            if (identityUsers != null)
            {
                await _appIdentityUserRepository.DeleteAsync(identityUsers!.Id, true);
            }


            //将新生成token存入数据库
            await _appIdentityUserRepository.AddAsync(tmpIdentityUser);


            var resAppToken = new AppToken
            {
                AccessToken = token,
                RefreshToken = refreshToken
            };
            var result = Task.FromResult<AppToken>(resAppToken);
            return await result;
        }

        public async Task<AppToken?> RefreshTokenAsync(AppToken appToken)
        {
            var jwtSetting = _configuration.GetSection("JwtSetting").Get<JwtSetting>();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtSetting.Issuer,
                ValidAudience = jwtSetting.Audience,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Secret))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(appToken.AccessToken, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
                throw new SecurityTokenException("token无效");
            }

            var userId = principal.FindFirstValue("UserId");

            if (userId != null)
            {
                var guserId = new Guid(userId);
                var user = _appIdentityUserRepository.Table.FirstOrDefault(x => x.UserId == guserId);
                if (user == null || user.RefreshToken != appToken.RefreshToken || user.RefreshTokenExpiration <= DateTime.Now)
                {
                    return null;
                    throw new Exception("传入的token或者refreshToken无效");
                }
                _appUser = _appUserRepository.Table.FirstOrDefault(x => x.Id == guserId);
                return await GenerateTokenAsync();
            }
            else
            {
                return null;
                throw new SecurityTokenException("token无效");
            }
        }

        public bool ValidateUserAsync(UserForAuth userForAuth)
        {
            _appUser = _appUserRepository.Table.FirstOrDefault(x => x.UserName == userForAuth.Username
             && x.PassWord == userForAuth.Password);

            return _appUser is null ? false : true;
        }


        private string GenerateRefreshToken()
        {
            // 创建一个随机的Token用做Refresh Token
            var rndNumber = new byte[32];

            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(rndNumber);

            return Convert.ToBase64String(rndNumber);
        }
    }

}