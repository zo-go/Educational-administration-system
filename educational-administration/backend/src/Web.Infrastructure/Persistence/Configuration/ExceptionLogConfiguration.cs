using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Domain.Log;
using Web.Infrastructure.Persistence.Configuration.Base;

namespace Web.Infrastructure.Persistence.Configuration
{
    public class ExceptionLogConfiguration : BaseEntityConfiguration<ExceptionLog>
    {
        public override void Configure(EntityTypeBuilder<ExceptionLog> builder)
        {
            base.Configure(builder);

            //设置表名
            builder.ToTable("exception_log");

            builder.Property(x => x.ShortMessage).HasColumnName("short_message").HasColumnOrder(1);
            builder.Property(x => x.FullMessage).HasColumnName("full_message").HasColumnOrder(2);
        }
    }
}