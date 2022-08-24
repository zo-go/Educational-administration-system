using Web.Api.Service;
using Web.Application.Common.Interface;
using Web.Infrastructure;
using Web.Infrastructure.Filter;
using Web.Infrastructure.Log;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//注册日志
builder.ConfigureLog();

//跨域
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        });
});

//访问HttpContext
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ISessionUserService, SessionUserService>();

// 注册基础设施层中的所有实例
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers(option =>
{
    option.Filters.Add(typeof(HttpGlobalExceptionFilter));
    option.Filters.Add(typeof(AuditActionLog));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthentication();      //鉴权
app.UseAuthorization();       //授权


app.MapControllers();

// 允许 PostgreSQL 使用 DateTime.Now 的时间格式
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

//种子数据
app.MigrateDatabase();
app.Run();
