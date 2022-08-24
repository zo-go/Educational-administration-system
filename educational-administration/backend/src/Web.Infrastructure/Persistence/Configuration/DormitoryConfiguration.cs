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
    public class DormitoryConfiguration : BaseEntityConfiguration<Dormitory>
    {
        public override void Configure(EntityTypeBuilder<Dormitory> builder)
        {
            base.Configure(builder);

            builder.ToTable("app_dormitory");

            builder.Property(x => x.BuildingNum).HasColumnName("building_num").HasColumnOrder(1);
            builder.Property(x => x.DormitoryNum).HasColumnName("dormitory_num").HasColumnOrder(2);
            builder.Property(x => x.StudentId).HasColumnName("student_id").HasColumnOrder(3);
            builder.Property(x => x.isDormAdmin).HasColumnName("is_dorm_admin").HasColumnOrder(4);
        }
    }
}