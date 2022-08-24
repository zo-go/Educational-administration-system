using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Entity
{
    public class ClassInfo : Base.BaseEntity
    {
        //班级名
        public string ClassName { get; set; } = null!;
        //班级编号
        public string ClassNum { get; set; } = null!;
        //专业序号
        public string SpecializedNum { get; set; } = null!;
        //辅导员id(账号id)
        public string CounselorId { get; set; } = null!;


    }
}