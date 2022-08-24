using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Application.ReqDto.ServerDto
{
    public class IntentionDTO
    {
        //学生Id
        public Guid StudentId { get; set; }
        //班级Id
        public Guid ClassId { get; set; }
    }
}