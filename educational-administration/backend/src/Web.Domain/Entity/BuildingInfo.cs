using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Entity
{
    public class BuildingInfo : Base.BaseEntity
    {

        //建筑名称
        public string BuildingName { get; set; } = null!;
        //编号
        public string BuildingNum { get; set; } = null!;
        //楼层
        public string floor { get; set; } = null!;

    }
}