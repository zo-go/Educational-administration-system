using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Entity
{
    public class Dormitory : Base.BaseEntity
    {
        //宿舍地址
        public string BuildingNum { get; set; } = null!;
        //宿舍号
        public int DormitoryNum { get; set; }
        //学生Id
        public string StudentId { get; set; } = null!;
        //是否为舍长
        public bool isDormAdmin { get; set; }
    }
}