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
    public class QuestionnaireOptionsConfiguration : BaseEntityConfiguration<QuestionnaireOptions>
    {
        public override void Configure(EntityTypeBuilder<QuestionnaireOptions
        > builder)
        {
            base.Configure(builder);

            builder.ToTable("app_questionnaire_options");

            builder.Property(x => x.QuestionnaireId).HasColumnName("questionnaire_id").HasColumnOrder(1);
            builder.Property(x => x.QuestionnaireQuestionId).HasColumnName("questionnaire_question_id").HasColumnOrder(2);
            builder.Property(x => x.OptionName).HasColumnName("option_name").HasColumnOrder(3);


        }
    }
}