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
    public class TextBookInfoConfiguration : BaseEntityConfiguration<TextBookInfo>
    {


        public override void Configure(EntityTypeBuilder<TextBookInfo> builder)
        {
            base.Configure(builder);

            builder.ToTable("app_textbook_info");
            builder.Property(x => x.TextBookName).HasColumnName("text_bookname").HasColumnOrder(1);
            builder.Property(x => x.Price).HasColumnName("price").HasColumnOrder(2);
            builder.Property(x => x.Press).HasColumnName("press").HasColumnOrder(3);
            builder.Property(x => x.ContactNumber).HasColumnName("contact_number").HasColumnOrder(4);
        }
    }
}