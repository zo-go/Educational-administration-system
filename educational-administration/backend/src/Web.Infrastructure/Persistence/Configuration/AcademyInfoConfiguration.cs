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
    public class AcademyInfoConfiguration : BaseEntityConfiguration<AcademyInfo>
    {
        public override void Configure(EntityTypeBuilder<AcademyInfo> builder)
        {
            base.Configure(builder);

            builder.ToTable("app_academy_info");
            builder.Property(x => x.AcademyNum).HasColumnName("academy_num").HasColumnOrder(1);
            builder.Property(x => x.AcademyName).HasColumnName("academy_name").HasColumnOrder(2);

        }
    }
}