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
    public class ClassInfoConfiguration : BaseEntityConfiguration<ClassInfo>
    {
        public override void Configure(EntityTypeBuilder<ClassInfo> builder)
        {
            base.Configure(builder);

            builder.ToTable("app_class_info");

            builder.Property(x => x.SpecializedNum).HasColumnName("specialized_num").HasColumnOrder(1);
            builder.Property(x => x.ClassNum).HasColumnName("class_num").HasColumnOrder(3);
            builder.Property(x => x.ClassName).HasColumnName("class_name").HasColumnOrder(2);
            builder.Property(x => x.CounselorId).HasColumnName("counselor_id").HasColumnOrder(4);

        }
    }
}