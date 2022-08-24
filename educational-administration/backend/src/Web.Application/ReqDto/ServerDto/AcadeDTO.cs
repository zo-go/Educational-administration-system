using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Application.ReqDto.ServerDto
{
    public class AcadeDTO
    {
        //学院序号
        public string? AcademyNum { get; set; }
        //学院名称
        public string AcademyName { get; set; } = null!;
    }
}