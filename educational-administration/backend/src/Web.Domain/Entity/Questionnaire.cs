using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Entity
{
    public class Questionnaire : Base.BaseEntity
    {
        //问卷主题
        public string QuestionnaireTheme { get; set; } = null!;
        //问卷标题
        public string QuestionnaireTitle { get; set; } = null!;
        //结束时间
        public DateTime EndTime { get; set; }

    }
}