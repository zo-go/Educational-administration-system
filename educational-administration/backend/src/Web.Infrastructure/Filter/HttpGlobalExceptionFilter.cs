using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.Application.Common.Interface;
using Web.Domain.Log;

namespace Web.Infrastructure.Filter
{
    public class HttpGlobalExceptionFilter : IAsyncExceptionFilter
    {
        private readonly IRepository<ExceptionLog> _ExceptionLogRes;

        public HttpGlobalExceptionFilter(IRepository<ExceptionLog> ExceptionLogRes)
        {
            _ExceptionLogRes = ExceptionLogRes;
        }


        public async Task OnExceptionAsync(ExceptionContext context)
        {
            //捕获全局异常，记入数据库日志
            await _ExceptionLogRes.AddAsync(new ExceptionLog
            {
                ShortMessage = context.Exception.Message,
                FullMessage = context.Exception.ToString()
            });


            context.ExceptionHandled = true;

            //打印 异常信息
            System.Console.WriteLine(string.Format("消息类型:{0}\n消息内容:{1}\n引发异常的方法:{2}\n引发异常源:{3}"
               , context.Exception.GetType().Name
               , context.Exception.Message
                , context.Exception.TargetSite
                , context.Exception.Source + context.Exception.StackTrace
                 ));

        }


    }
}