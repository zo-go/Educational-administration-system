using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Application.ReqDto
{
    public class UserForAuth
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}