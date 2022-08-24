using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Web.Application.Common.Interface;
using Web.Application.Common.Interface.IServer;
using Web.Application.Configuration;
using Web.Application.Utils;
using Web.Infrastructure.Identity;
using Web.Infrastructure.Persistence;
using Web.Infrastructure.Persistence.Repositoty;
using Web.Infrastructure.Upload;
using Web.Services.Services;

namespace Web.Infrastructure
{
    public static class DependencyInjectionDemo
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TeaachAdminDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("PostgreSQL"),
                    b => b.MigrationsAssembly(typeof(TeaachAdminDbContext).Assembly.FullName)));



            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddScoped<IAppFileUploadService, AppFileUploadService>();

            // 注册 Services 层实现的接口
            services.AddScoped<IAcademyInfoServices, AcademyInfoServices>();
            services.AddScoped<IBuildingServices, BuildingServices>();
            services.AddScoped<IClassServices, ClassServices>();
            services.AddScoped<ICourseServices, CourseServices>();
            services.AddScoped<ICurriCulumServices, CurriCulumServices>();
            services.AddScoped<IDormitoryServices, DormitoryServices>();
            services.AddScoped<IIntentionServices, IntentionServices>();
            services.AddScoped<IQuestionnaireRecordServices, QuestionnaireRecordServices>();
            services.AddScoped<IQuestionnaireResServices, QuestionnaireResServices>();
            services.AddScoped<IQuestionnaireServices, QuestionnaireServices>();
            services.AddScoped<IRoleServices, RoleServices>();
            services.AddScoped<IScoreServices, ScoreServices>();
            services.AddScoped<ISpecializedServices, SpecializedServices>();
            services.AddScoped<IStudentServices, StudentServices>();
            services.AddScoped<ITeacherServices, TeacherServices>();
            services.AddScoped<ITextBookServices, TextBookServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IQuestionnaireOptionServices, QuestionnaireOptionServices>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(option =>
               {
                   option.RequireHttpsMetadata = false;//配置是否为https协议
                   option.SaveToken = true;//配置token是否保存在api上下文

                   var tokenParameter = configuration.GetSection("JwtSetting").Get<JwtSetting>();
                   option.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,

                       ValidIssuer = tokenParameter.Issuer,
                       ValidAudience = tokenParameter.Audience,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenParameter.Secret))
                   };
                   option.Events = new JwtBearerEvents
                   {
                       //token验证失败后执行
                       OnChallenge = context =>
                       {
                           // 跳过默认响应逻辑
                           context.HandleResponse();
                           // 自定义401时返回的信息
                           var result = JsonHelper.SerializeObject(new { Code = "401", Message = "token验证失败" });
                           context.Response.ContentType = "application/json";
                           //验证失败返回401
                           context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                           context.Response.WriteAsync(result);
                           return Task.FromResult(0);
                       }
                   };
               });
            return services;
        }
    }
}