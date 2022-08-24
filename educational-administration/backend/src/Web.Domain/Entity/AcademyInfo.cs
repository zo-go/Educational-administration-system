using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Entity
{
    public class AcademyInfo : Base.BaseEntity
    {
        //学院序号
        public string AcademyNum { get; set; } = null!;
        //学院名称
        public string AcademyName { get; set; } = null!;

    }
}