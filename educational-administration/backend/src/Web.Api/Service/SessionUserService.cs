using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Application.Common.Interface;

namespace Web.Api.Service
{
    public class SessionUserService : ISessionUserService
    {
        private readonly IHttpContextAccessor _httpContext;
        public SessionUserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public Guid? UserId
        {
            get
            {
                var tmp = _httpContext.HttpContext?.User.FindFirstValue("UserId");
                // var tmp = _httpContext.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == "UserId");
                if (tmp != null)
                {
                    var x = new Guid(tmp);
                    return x;
                }
                else
                {
                    return null;
                }
            }
        }
        public string? Username
        {
            get
            {
                var tmp = _httpContext.HttpContext?.User.FindFirstValue("Username");
                return tmp;
            }
        }
    }
}