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
    public class AppUploadFileInfoConfiguration : BaseEntityConfiguration<AppUploadFileInfo>
    {
        public override void Configure(EntityTypeBuilder<AppUploadFileInfo> builder)
        {
            base.Configure(builder);

            builder.ToTable("app_uploadfile_info");


            builder.Property(x => x.OriginFileName).HasColumnName("origin_filename").HasColumnOrder(1);
            builder.Property(x => x.CurrentFileName).HasColumnName("current_filename").HasColumnOrder(2);
            builder.Property(x => x.RelativePath).HasColumnName("relative_path").HasColumnOrder(3);
            builder.Property(x => x.FileType).HasColumnName("file_type").HasColumnOrder(4);
            builder.Property(x => x.UserId).HasColumnName("user_id").HasColumnOrder(5);

        }
    }
}