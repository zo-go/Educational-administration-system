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
    public class ScoreInfoConfiguration : BaseEntityConfiguration<ScoreInfo>
    {
        public override void Configure(EntityTypeBuilder<ScoreInfo> builder)
        {
            base.Configure(builder);

            builder.ToTable("app_score_info");

            builder.Property(x => x.StudentId).HasColumnName("student_id").HasColumnOrder(1);
            builder.Property(x => x.SubjectId).HasColumnName("subject_id").HasColumnOrder(2);
            builder.Property(x => x.Score).HasColumnName("score").HasColumnOrder(3);
            builder.Property(x => x.SemesterId).HasColumnName("semester_id").HasColumnOrder(4);
            builder.Property(x => x.ClassId).HasColumnName("class_id").HasColumnOrder(5);

        }
    }
}