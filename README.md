# 一、ReZero说明
是一款.NET中间件,让你无需写任何代码也能实现CRUD，也可以发布成exe独立使用于非.NET用户
创建接口
![输入图片说明](/sunkaixuan/ReZero/raw/master/image4.png)
查看创建后的接口
![输入图片说明](image2.png)


# 二、功能介绍
在线数据库和表
在线创建API接口 、接口文档 和接口调试

 
# 三、非.NET用户教程
通过下载EXE运行
https://gitee.com/sunkaixuan/ReZero/releases/tag/1.0.0.16


# 四、.NET用户教程

## 4.1 Nuget安装
```cs
Rezero.Api 
``` 
## 4.2 一行代码配置
新建一个.NET6+ WEB API
只需要注入一行代码就能使用 Rezero API

```cs
/***对现有代码没有任何影响***/

//注册：注册超级API服务
builder.Services.AddReZeroServices(api =>
{
    //启用超级API
    api.EnableSuperApi();//默认载体为sqlite ，有重载可以配置数据库

});
//写在builder.Build前面就行只需要一行
var app = builder.Build();

```
## 4.3使用ReZero
启动项目直接访问地址就行了
http://localhost:5267/rezero 

## 4.4 jwt授权

```cs
//注册：注册超级API服务
builder.Services.AddReZeroServices(api =>
{
    //启用超级API
    api.EnableSuperApi(new SuperAPIOptions()
    { 
        InterfaceOptions = new InterfaceOptions()
        {
            SuperApiAop = new JwtAop()//授权拦截器
        }
    }); ;

});
public class JwtAop : DefaultSuperApiAop
{
    public async override Task OnExecutingAsync(InterfaceContext aopContext)
    {
        //// 尝试验证JWT  
        //var authenticateResult = await aopContext.HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
        //if (!authenticateResult.Succeeded)
        //{
        //    // JWT验证失败，返回401 Unauthorized或其他适当的响应  
        //    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        //    await context.Response.WriteAsync("Unauthorized");
        //    return;
        //}
        await base.OnExecutingAsync(aopContext);
    }
    public async override Task OnExecutedAsync(InterfaceContext aopContext)
    {
        await base.OnExecutedAsync(aopContext);
    }
    public async override Task OnErrorAsync(InterfaceContext aopContext)
    {
        await base.OnErrorAsync(aopContext);
    }
}
```
# 五、打赏作者
从项目启动时前就有人赞助我开发了
目前已经有累计过万的打赏了很多人都需要这个功能
希望大家多多支持，对标的是收费软件
![输入图片说明](image3.png)
