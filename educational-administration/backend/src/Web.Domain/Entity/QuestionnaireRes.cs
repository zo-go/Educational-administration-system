using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Entity
{
    public class QuestionnaireRes : Base.BaseEntity
    {
        //关联问卷主表id
        public Guid QuestionnaireId { get; set; }

        //关联问题id
        public Guid QuestionnaireQuestionId { get; set; }

        //关联选项id
        public Guid OptionId { get; set; }

        //选项名称 选项值
        public string OptionValue { get; set; } = null!;

    }
}