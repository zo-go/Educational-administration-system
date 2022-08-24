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
    public class AppUserConfiguration : BaseEntityConfiguration<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            base.Configure(builder);

            builder.ToTable("app_user");

            builder.Property(x => x.UserName).HasColumnName("user_name").HasColumnOrder(1);
            builder.Property(x => x.PassWord).HasColumnName("password").HasColumnOrder(2);
            builder.Property(x => x.RoleId).HasColumnName("role_id").HasColumnOrder(3);
            builder.Property(x => x.AvatarId).HasColumnName("user_avatar_id").HasColumnOrder(4);

        }
    }
}