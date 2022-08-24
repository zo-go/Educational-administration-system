using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Entity
{
    public class QuestionnaireRecord : Base.BaseEntity
    {

        //关联问卷主题Id
        public Guid QuestionnaireID { get; set; }
        //问卷问题
        public string QuestionnaireQuestion { get; set; } = null!;
        //选项描述
        public string? OptionDescribe { get; set; }
        //问卷选项类型 单选多选填空等
        public string QuestionnaireOptionType { get; set; } = null!;
        //是否必填
        public int QuestionnaireFlag { get; set; }

    }
}