using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Entity
{
    public class IntentionInfo : Base.BaseEntity
    {
        //学生Id
        public Guid StudentId { get; set; }
        //班级Id
        public Guid ClassId { get; set; }

    }
}