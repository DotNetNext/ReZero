# ReZero
是一款.NET中间件，也可以发布成exe独立使用于非.NET用户 
他的功能是在线创建表、建库、API接口 （包含文档和接口调试） 

# 安装
NUGET  Rezero.Api 
只需要注入一行代码就能使用 Rezero API

```cs
using Microsoft.AspNetCore.Components.Forms;
using ReZero;
using ReZero.SuperAPI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//注册：注册超级API服务
builder.Services.AddReZeroServices(api =>
{
    //启用超级API
    api.EnableSuperApi(new SuperAPIOptions()
    {
        DatabaseOptions = new DatabaseOptions()//DatabaseOptions 可以不设置默认Sqlite为载体
        {
            ConnectionConfig = new SuperAPIConnectionConfig()
            {
                ConnectionString = "Server=.;Database=SuperAPI;User Id=sa;Password=sasa;",
                DbType = SqlSugar.DbType.SqlServer,
            },
        }
    });

});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

```
# 使用ReZero
直接访问地址就行了
http://localhost:5267/rezero 
 
# 功能预览

![输入图片说明](image2.png)