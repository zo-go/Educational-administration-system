using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Domain.Log;
using Web.Domain.Entity;
namespace Web.Infrastructure.Persistence
{
    public class TeaachAdminDbContext : DbContext
    {
        public TeaachAdminDbContext(DbContextOptions<TeaachAdminDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // 应用当前Assembly中定义的所有的Configurations，就不需要一个一个去写了。
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public DbSet<ExceptionLog> exception_log => Set<ExceptionLog>();
        public DbSet<AuditLog> audit_log => Set<AuditLog>();
        public DbSet<AcademyInfo> app_academy_info => Set<AcademyInfo>();
        public DbSet<AppUser> app_user => Set<AppUser>();
        public DbSet<AppRole> app_role => Set<AppRole>();
        public DbSet<AppIdentityUser> app_identity_user => Set<AppIdentityUser>();
        public DbSet<IntentionInfo> IntentionInfo => Set<IntentionInfo>();
        public DbSet<AppUploadFileInfo> app_uploadfile_info => Set<AppUploadFileInfo>();
        public DbSet<ClassInfo> app_class_info => Set<ClassInfo>();
        public DbSet<CourseInfo> app_course_Info => Set<CourseInfo>();
        public DbSet<Dormitory> app_dormitory => Set<Dormitory>();
        public DbSet<Questionnaire> app_questionnaire => Set<Questionnaire>();
        public DbSet<QuestionnaireRecord> app_questionnaire_record => Set<QuestionnaireRecord>();
        public DbSet<QuestionnaireRes> app_questionnaire_res => Set<QuestionnaireRes>();
        public DbSet<QuestionnaireOptions> app_questionnaire_option => Set<QuestionnaireOptions>();
        public DbSet<StudentInfo> app_student_info => Set<StudentInfo>();
        public DbSet<ScoreInfo> app_score_info => Set<ScoreInfo>();
        public DbSet<SpecializedInfo> app_specialized_info => Set<SpecializedInfo>();
        public DbSet<CurriCulum> app_curriculums => Set<CurriCulum>();
        public DbSet<TextBookInfo> app_textbook_info => Set<TextBookInfo>();
        public DbSet<TeacherInfo> app_teacher_info => Set<TeacherInfo>();
        public DbSet<BuildingInfo> app_building_info => Set<BuildingInfo>();
        public DbSet<TermInfo> app_term_info => Set<TermInfo>();

    }
}