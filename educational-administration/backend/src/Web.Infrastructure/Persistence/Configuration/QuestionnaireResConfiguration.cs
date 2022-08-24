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
    public class QuestionnaireResConfiguration : BaseEntityConfiguration<QuestionnaireRes>
    {
        public override void Configure(EntityTypeBuilder<QuestionnaireRes> builder)
        {
            base.Configure(builder);

            builder.ToTable("app_questionnaire_res");

            builder.Property(x => x.QuestionnaireId).HasColumnName("questionnaire_id").HasColumnOrder(1);
            builder.Property(x => x.QuestionnaireQuestionId).HasColumnName("questionnaire_question_id").HasColumnOrder(2);
            builder.Property(x => x.OptionId).HasColumnName("option_id").HasColumnOrder(3);
            builder.Property(x => x.OptionValue).HasColumnName("option_value").HasColumnOrder(4);


        }
    }
}