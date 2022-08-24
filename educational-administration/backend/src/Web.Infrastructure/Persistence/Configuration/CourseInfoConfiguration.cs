using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Domain.Entity;
using Web.Infrastructure.Persistence.Configuration.Base;

namespace Web.Infrastructure.Persistence.Configuration
{
    public class CourseInfoConfiguration : BaseEntityConfiguration<CourseInfo>
    {
        public override void Configure(EntityTypeBuilder<CourseInfo> builder)
        {
            base.Configure(builder);
            builder.ToTable("app_course_info");

            builder.Property(x => x.CourseName).HasColumnName("course_name").HasColumnOrder(1);
            builder.Property(x => x.TextBookId).HasColumnName("textbook_id").HasColumnOrder(2);
            builder.Property(x => x.isPublicCourses).HasColumnName("is_public_courses").HasColumnOrder(3);
            builder.Property(x => x.SpecializedNum).HasColumnName("specialized_num").HasColumnOrder(4);
            builder.Property(x => x.TeacherId).HasColumnName("teacher_id").HasColumnOrder(5);

        }
    }
}