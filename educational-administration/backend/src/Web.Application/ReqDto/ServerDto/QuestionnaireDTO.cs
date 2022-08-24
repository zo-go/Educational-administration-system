using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Application.ReqDto.ServerDto
{
    public class QuestionnaireDTO
    {
        //问卷主题
        public string QuestionnaireTheme { get; set; } = null!;
        //问卷标题
        public string QuestionnaireTitle { get; set; } = null!;
        //结束时间
        public string? EndTime { get; set; }
    }
}