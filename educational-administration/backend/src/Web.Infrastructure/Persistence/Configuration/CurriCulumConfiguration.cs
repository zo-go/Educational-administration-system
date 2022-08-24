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
    public class CurriCulumConfiguration : BaseEntityConfiguration<CurriCulum>
    {
        public override void Configure(EntityTypeBuilder<CurriCulum> builder)
        {
            base.Configure(builder);

            builder.ToTable("app_curriculum");

            builder.Property(x => x.CurriCulumData).HasColumnName("curriculum_data").HasColumnOrder(1);
            builder.Property(x => x.SpecializedName).HasColumnName("specialized_name").HasColumnOrder(2);

        }
    }
}