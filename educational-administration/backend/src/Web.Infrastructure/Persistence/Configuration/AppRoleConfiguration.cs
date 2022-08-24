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
    public class AppRoleConfiguration : BaseEntityConfiguration<AppRole>
    {
        public override void Configure(EntityTypeBuilder<AppRole> builder)
        {
            base.Configure(builder);

            builder.ToTable("app_role");

            builder.Property(x => x.RoleName).HasColumnName("role_name").HasColumnOrder(1);
        }
    }
}