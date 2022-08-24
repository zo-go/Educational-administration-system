using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Entity
{
    public class SpecializedInfo : Base.BaseEntity
    {
        //学院序号 (归属哪个学院)
        public string AcademyNum { get; set; } = null!;

        //专业名称
        public string SpecializedName { get; set; } = null!;

        //专业序号
        public string SpecializedNum { get; set; } = null!;


    }
}