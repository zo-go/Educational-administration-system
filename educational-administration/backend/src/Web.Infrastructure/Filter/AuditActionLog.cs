using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.Application.Common.Interface;
using Web.Domain.Log;
using Web.Application.Utils;

namespace Web.Infrastructure.Filter
{

    public class AuditActionLog : IAsyncActionFilter
    {
        private readonly IRepository<AuditLog> _auditlog;
        private readonly ISessionUserService _sessionUserService;

        public AuditActionLog(IRepository<AuditLog> auditlog, ISessionUserService sessionUserService)
        {
            _auditlog = auditlog;
            _sessionUserService = sessionUserService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            //接口Type
            var type = (context.ActionDescriptor as ControllerActionDescriptor)?.ControllerTypeInfo.AsType();
            //方法信息
            var method = (context.ActionDescriptor as ControllerActionDescriptor)?.MethodInfo;
            //方法参数
            var arguments = context.ActionArguments;
            //开始计时
            var stopwatch = Stopwatch.StartNew();
            var auditInfo = new AuditLog
            {
                // UserInfo = _sessionUserService.Username,
                UserInfo = "佚名",
                ServiceName = type != null ? type.FullName : "",
                MethodName = method?.Name,
                ////请求参数转Json
                Parameters = JsonHelper.SerializeObject(arguments),
                ExecutionTime = DateTime.Now,
                BrowserInfo = context.HttpContext.Request.Headers["User-Agent"].ToString(),
                ClientIpAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString(),
                ClientName = System.Net.Dns.GetHostName()  //获取当前电脑名
            };

            ActionExecutedContext? result = null;
            try
            {
                result = await next();
                if (result.Exception != null && !result.ExceptionHandled)
                {
                    auditInfo.Exception = result.Exception.ToString();
                }
            }
            catch (Exception ex)
            {
                auditInfo.Exception = ex.ToString();
                throw;
            }
            finally
            {
                stopwatch.Stop();
                auditInfo.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);

                if (result != null)
                {
                    switch (result.Result)
                    {
                        case ObjectResult objectResult:
                            auditInfo.ReturnValue = JsonHelper.SerializeObject(objectResult.Value);
                            break;

                        case JsonResult jsonResult:
                            auditInfo.ReturnValue = JsonHelper.SerializeObject(jsonResult.Value);
                            break;

                        case ContentResult contentResult:
                            auditInfo.ReturnValue = contentResult.Content;
                            break;
                    }
                }

                //打印 审计日志插入的值
                var auditDto = JsonHelper.SerializeObject(auditInfo);
                // System.Console.WriteLine(auditDto);

                //保存审计日志
                await _auditlog.AddAsync(auditInfo);

            }
        }
    }

}