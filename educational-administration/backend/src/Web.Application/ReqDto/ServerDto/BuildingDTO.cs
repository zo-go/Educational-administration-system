using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Application.ReqDto.ServerDto
{
    public class BuildingDTO
    {
        //建筑名称
        public string BuildingName { get; set; } = null!;
        //编号
        public string BuildingNum { get; set; } = null!;
        //楼层
        public string Floor { get; set; } = null!;
    }
}