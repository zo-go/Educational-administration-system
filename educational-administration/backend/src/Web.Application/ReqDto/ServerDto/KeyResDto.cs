using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Application.ReqDto.ServerDto
{
    public class KeyResDto
    {
        public Guid QuestionnaireId { get; set; }
        public Guid CreatedBy { get; set; }
    }
}