using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Entity
{
    public class AppUser : Base.BaseEntity
    {

        /// 用户名(账号)
        public string UserName { get; set; } = null!;
        /// 密码
        public string PassWord { get; set; } = null!;

        public Guid RoleId { get; set; }
        /// 头像ID
        public Guid? AvatarId { get; set; }
        public string? OpenId { get; set; }
    }
}