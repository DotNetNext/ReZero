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


//�����ؿɻ�json�ļ�
var apiObj = SuperAPIOptions.GetOptions();
builder.Services.AddReZeroServices(api =>
{ 

    //IOCҵ���������Ҫ�����м��̼�
    var assemblyList = Assembly.GetExecutingAssembly()
                        .GetAllDependentAssemblies(it => it.Contains("SuperAPITest"))
                        .ToArray(); 

    apiObj.DependencyInjectionOptions = new DependencyInjectionOptions(assemblyList);
     
    //���ó���API
    api.EnableSuperApi(apiObj); 

}); 
//ע��: ҵ������õ�SqlSugar���󣨷ǵʹ���ģʽ�ò��������
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
