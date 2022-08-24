using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Application.Configuration
{
    public class JwtSetting
    {
        public string Secret { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public long AccessExpiration { get; set; }
        public long RefreshExpiration { get; set; }
    }
}