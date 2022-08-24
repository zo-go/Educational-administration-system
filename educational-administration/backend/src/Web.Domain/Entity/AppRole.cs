using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Entity
{
    public class AppRole : Base.BaseEntity
    {
        /// <summary>
        /// 角色名
        /// </summary>
        /// <value></value>
        public string RoleName { get; set; } = null!;

    }
}