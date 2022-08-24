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
    public class StudentInfoConfiguration : BaseEntityConfiguration<StudentInfo>
    {
        public override void Configure(EntityTypeBuilder<StudentInfo> builder)
        {
            base.Configure(builder);

            builder.ToTable("app_student_info");

            builder.Property(x => x.StudentId).HasColumnName("student_id").HasColumnOrder(1);
            builder.Property(x => x.StudentName).HasColumnName("student_name").HasColumnOrder(2);
            builder.Property(x => x.Sex).HasColumnName("sex").HasColumnOrder(3);
            builder.Property(x => x.IdNumber).HasColumnName("id_number").HasColumnOrder(4);
            builder.Property(x => x.ClassId).HasColumnName("class_id").HasColumnOrder(5);
            builder.Property(x => x.PhoneNumber).HasColumnName("phone_number").HasColumnOrder(6);
            builder.Property(x => x.WeChat).HasColumnName("we_chat").HasColumnOrder(7);
            builder.Property(x => x.qqNumber).HasColumnName("qq_number").HasColumnOrder(8);
            builder.Property(x => x.Address).HasColumnName("address").HasColumnOrder(9);
            builder.Property(x => x.FatherName).HasColumnName("father_name").HasColumnOrder(10);
            builder.Property(x => x.MotherName).HasColumnName("mother_name").HasColumnOrder(11);
            builder.Property(x => x.ContactDetails).HasColumnName("contact_details").HasColumnOrder(12);
            builder.Property(x => x.EnrollmentTime).HasColumnName("enrollment_time").HasColumnOrder(13);
        }

    }
}