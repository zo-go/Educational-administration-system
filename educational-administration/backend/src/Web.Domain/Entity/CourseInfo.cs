using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Entity
{
    public class CourseInfo : Base.BaseEntity
    {
        //课程名
        public string CourseName { get; set; } = null!;

        //教材Id
        public Guid TextBookId { get; set; }

        //是否公共课
        public bool isPublicCourses { get; set; }

        //专业序号
        public string? SpecializedNum { get; set; }

        //任课老师
        public string? TeacherId { get; set; }
    }
}