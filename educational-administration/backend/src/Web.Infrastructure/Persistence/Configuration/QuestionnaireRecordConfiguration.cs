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
    public class QuestionnaireRecordConfiguration : BaseEntityConfiguration<QuestionnaireRecord>
    {
        public override void Configure(EntityTypeBuilder<QuestionnaireRecord> builder)
        {
            base.Configure(builder);

            builder.ToTable("app_questionnaire_record");

            builder.Property(x => x.QuestionnaireID).HasColumnName("questionnaire_id").HasColumnOrder(1);
            builder.Property(x => x.QuestionnaireOptionType).HasColumnName("questionnaire_option_type").HasColumnOrder(2);
            builder.Property(x => x.QuestionnaireQuestion).HasColumnName("questionnaire_question").HasColumnOrder(3);
            builder.Property(x => x.OptionDescribe).HasColumnName("option_describe").HasColumnOrder(4);
            builder.Property(x => x.QuestionnaireFlag).HasColumnName("questionnaire_flag").HasColumnOrder(5);


        }
    }
}