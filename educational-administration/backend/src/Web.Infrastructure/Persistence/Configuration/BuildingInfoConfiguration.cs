using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Infrastructure.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Web.Domain.Entity;


namespace Web.Infrastructure.Persistence.Configuration
{
    public class BuildingInfoConfiguration : BaseEntityConfiguration<BuildingInfo>
    {
        public override void Configure(EntityTypeBuilder<BuildingInfo> builder)
        {
            base.Configure(builder);

            builder.ToTable("app_building_info");

            builder.Property(x => x.BuildingNum).HasColumnName("building_num").HasColumnOrder(1);
            builder.Property(x => x.BuildingName).HasColumnName("building_name").HasColumnOrder(2);
            builder.Property(x => x.floor).HasColumnName("floor").HasColumnOrder(3);

        }
    }
}