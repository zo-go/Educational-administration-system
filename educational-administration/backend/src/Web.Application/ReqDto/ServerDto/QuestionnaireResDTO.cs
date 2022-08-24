using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Application.ReqDto.ServerDto
{
    public class QuestionnaireResDTO
    {
        //问卷Id
        public Guid QuestionnaireId { get; set; }

        //问卷问题Id
        public Guid QuestionnaireQuestionId { get; set; }

        //关联问卷选项Id
        public Guid? OptionId { get; set; }

        //选项值
        public string? OptionValue { get; set; }

    }

    public class ResListDto
    {
        public string StudentNum { get; set; } = null!;
        public List<QuestionnaireResDTO> resDtoList { get; set; } = new List<QuestionnaireResDTO>();
    }
}