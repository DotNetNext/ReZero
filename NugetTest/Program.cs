using ReZero;
using ReZero.SuperAPI;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

 
//Register: Register the super API service
//注册：注册超级API服务
builder.Services.AddReZeroServices(api =>
{
    //启用超级API
    //有重载可换json文件
    var apiObj = SuperAPIOptions.GetOptions();

    apiObj!.DependencyInjectionOptions = new DependencyInjectionOptions(Assembly.GetExecutingAssembly());
    //启用超级API
    api.EnableSuperApi(apiObj);

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#if !DEBUG 
try 
	{	        
		    // 假设您的应用程序在本地5000端口上运行  
            string url = "http://localhost:65000/rezero/dynamic_interface.html?InterfaceCategoryId=200100";
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            }); 
	}
	catch (global::System.Exception)
	{
     //docker中不能打开浏览器，出错不处理
	}
#endif
// 启动默认的网页浏览器并打开指定的URL  

app.Run();
