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
    public class SpecializedInfoConfiguration : BaseEntityConfiguration<SpecializedInfo>
    {
        public override void Configure(EntityTypeBuilder<SpecializedInfo> builder)
        {
            base.Configure(builder);

            builder.ToTable("app_specialized_info");

            builder.Property(x => x.AcademyNum).HasColumnName("academy_num").HasColumnOrder(1);
            builder.Property(x => x.SpecializedName).HasColumnName("specialized_name").HasColumnOrder(2);
            builder.Property(x => x.SpecializedNum).HasColumnName("specialized_num").HasColumnOrder(3);


        }
    }
}