using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Entity
{
    public class ScoreInfo : Base.BaseEntity
    {
        //学生Id 
        public Guid StudentId { get; set; }
        //课程Id 指向课程表ID
        public Guid SubjectId { get; set; }
        //成绩(分数)
        public string? Score { get; set; }
        //学期Id 指向学期表
        public Guid SemesterId { get; set; }
        //班级Id 指向班级表Id
        public Guid ClassId { get; set; }
    }
}