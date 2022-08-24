using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Application.ReqDto.ServerDto
{
    public class OptionDTO
    {

        //问卷主题表Id
        public Guid QuestionnaireID { get; set; }

        //问卷问题Id
        public Guid QuestionnaireQuestionId { get; set; }

        public string OptionName { get; set; } = null!;
    }
}