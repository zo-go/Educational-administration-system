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
    public class QuestionnaireConfiguration : BaseEntityConfiguration<Questionnaire>
    {
        public override void Configure(EntityTypeBuilder<Questionnaire> builder)
        {
            base.Configure(builder);

            builder.ToTable("app_questionnaire");

            builder.Property(x => x.QuestionnaireTheme).HasColumnName("questionnaire_theme").HasColumnOrder(1);
            builder.Property(x => x.QuestionnaireTitle).HasColumnName("questionnaire_title").HasColumnOrder(2);
            builder.Property(x => x.EndTime).HasColumnName("end_time").HasColumnOrder(3);

        }
    }
}