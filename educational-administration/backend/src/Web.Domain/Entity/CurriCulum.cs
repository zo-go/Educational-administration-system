using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Entity
{
    public class CurriCulum : Base.BaseEntity
    {
        /// <summary>
        /// 排课表 数据格式应为扁平化后的课表
        /// </summary>
        /// <value></value>
        public string? CurriCulumData { get; set; }
        /// <summary>
        /// 课表所属专业序号
        /// </summary>
        /// <value></value>
        public string? SpecializedName { get; set; }
    }
}