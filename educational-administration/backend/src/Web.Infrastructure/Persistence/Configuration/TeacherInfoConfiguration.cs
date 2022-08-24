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
    public class TeacherInfoConfiguration : BaseEntityConfiguration<TeacherInfo>
    {
        public override void Configure(EntityTypeBuilder<TeacherInfo> builder)
        {
            base.Configure(builder);

            builder.ToTable("app_teacher_info");

            builder.Property(x => x.WorkNumber).HasColumnName("work_number").HasColumnOrder(1);
            builder.Property(x => x.TeacherName).HasColumnName("teacher_name").HasColumnOrder(2);
            builder.Property(x => x.Sex).HasColumnName("sex").HasColumnOrder(3);
            builder.Property(x => x.IdNumber).HasColumnName("id_number").HasColumnOrder(5);
            builder.Property(x => x.PhoneNumber).HasColumnName("phone_number").HasColumnOrder(10);
            builder.Property(x => x.WeChat).HasColumnName("we_chat").HasColumnOrder(7);
            builder.Property(x => x.qqNumber).HasColumnName("qq_number").HasColumnOrder(8);
            builder.Property(x => x.Address).HasColumnName("address").HasColumnOrder(9);
            builder.Property(x => x.AcademyNum).HasColumnName("academy_num").HasColumnOrder(6);

        }
    }
}