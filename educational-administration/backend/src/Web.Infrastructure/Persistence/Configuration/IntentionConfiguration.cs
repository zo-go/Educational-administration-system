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
    public class IntentionConfiguration : BaseEntityConfiguration<IntentionInfo>
    {
        public override void Configure(EntityTypeBuilder<IntentionInfo> builder)
        {
            base.Configure(builder);

            builder.ToTable("app_intention_info");

            builder.Property(x => x.ClassId).HasColumnName("class_id").HasColumnOrder(1);
            builder.Property(x => x.StudentId).HasColumnName("student_id").HasColumnOrder(2);

        }
    }
}