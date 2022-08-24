using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.ReqDto;
using Web.Application.ResDto;

namespace Web.Application.Common.Interface
{
    public interface IIdentityService
    {
        bool ValidateUserAsync(UserForAuth userForAuth);

        Task<AppToken> GenerateTokenAsync();

        Task<AppToken?> RefreshTokenAsync(AppToken appToken);
    }
}