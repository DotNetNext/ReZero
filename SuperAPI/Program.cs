using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Forms;
using ReZero;
using ReZero.Configuration;
using ReZero.SuperAPI;
using SqlSugar;
using SuperAPITest;
using System.Reflection;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//有重载可换json文件
var apiObj = SuperAPIOptions.GetOptions();
builder.Services.AddReZeroServices(api =>
{ 

    //IOC业务等所有需要的所有集程集
    var assemblyList = Assembly.GetExecutingAssembly()
                        .GetAllDependentAssemblies(it => it.Contains("SuperAPITest"))
                        .ToArray(); 

    apiObj.DependencyInjectionOptions = new DependencyInjectionOptions(assemblyList);
     
    //启用超级API
    api.EnableSuperApi(apiObj); 

}); 
//注册: 业务代码用的SqlSugar对象（非低代码模式用不着这个）
builder.Services.AddScoped<ISqlSugarClient>(it =>
{
    return new SqlSugarClient(new ConnectionConfig()
    {
        DbType = apiObj.DatabaseOptions!.ConnectionConfig!.DbType,
        ConnectionString = apiObj.DatabaseOptions?.ConnectionConfig?.ConnectionString,
        IsAutoCloseConnection = true
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
