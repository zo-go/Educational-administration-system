Web.Api:访问层：提供HTTP访问入口，引用Web.Services、Web.Application、Web.Infrastructure

Web.Application：应用层 定义接口

Web.Services：业务层 实现接口方法

Web.Domain:领域层 纯净的模型

Web.Infrastructure:基础层 外部包引入与对接


返回的状态值：

200：请求成功  
400：传入格式错误的数据  
401：登录验证过期  
402：请求失败  
405：响应失败  
406：超时请求


返回示例:
~~~

{
    code=200，

    msg="获取成功！"，

    data:[{}]

}
~~~

分页返回值
~~~
                code = 200,

                msg = "获取数据成功",

                data = key,

                page = new PageDto

                {

                    pageIndex = pageIndex,

                    pageSize = pageSize,

                    OnThisPage = key.Count(),

                    Count = entity.Count()
                }
~~~