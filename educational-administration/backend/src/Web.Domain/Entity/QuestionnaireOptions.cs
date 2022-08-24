using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Entity
{
    public class QuestionnaireOptions : Base.BaseEntity
    {

        //关联问卷主表id
        public Guid QuestionnaireId { get; set; }

        //关联问题id
        public Guid QuestionnaireQuestionId { get; set; }

        //选项名称 
        public string OptionName { get; set; } = null!;

    }
}