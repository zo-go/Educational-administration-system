using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Web.Infrastructure.Log
{
    public static class ConfigureLogProvider
    {
        public static void ConfigureLog(this WebApplicationBuilder builder)
        {
            if (builder.Configuration.GetValue<bool>("SaveLogToFile"))
            {
                // 配置同时输出到控制台和文件，并且指定文件名和文件转储方式（形如log-20211219.txt格式），转储文件保留的天数为15天，以及日志格式
                // 配置Enrich.FromLogContext()的目的是为了从日志上下文中获取一些关键信息诸如用户ID或请求ID，我们的应用中暂时不使用这些。
                Serilog.Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    // .WriteTo.File(
                    //     "logs/log-.txt",    //路径
                    //     outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",  //日志输出模板
                    //     rollingInterval: RollingInterval.Day,               //创建文件的类别
                    //     retainedFileCountLimit: 15)                         //设置日志文件个数最大值，默认31，意思就是只保留最近的31个日志文件,等于null时永远保留文件
                    .CreateLogger();
            }
            else
            {
                // 仅配置控制台日志
                Serilog.Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .CreateLogger();
            }

            // 使用Serilog作为日志框架，注意这里和.NET 5及之前的版本写法是不太一样的。
            builder.Host.UseSerilog();
        }
    }
}