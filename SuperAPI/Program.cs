using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.IdentityModel.Tokens;
using ReZero;
using ReZero.Configuration;
using ReZero.SuperAPI;
using SqlSugar;
using SuperAPITest;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 

//ע��db: �����д������Բ�ע��
builder.Services.AddScoped<ISqlSugarClient>(it =>
{
    var config = ApiConfiguration.GetJsonValue<ReZeroJson>("ReZero");
    return new SqlSugarClient(new ConnectionConfig()
    {
        DbType = config!.BasicDatabase!.DbType,
        ConnectionString = config!.BasicDatabase!.ConnectionString,
        IsAutoCloseConnection = true
    });
});
//builder.Services.AddCors();


//ע��ReZero.Api
builder.Services.AddReZeroServices(api =>
{

    //�����ؿɻ�json�ļ�
    var apiObj = SuperAPIOptions.GetOptions();

    //IOCҵ���������Ҫ�����м��̼�
    var assemblyList = Assembly.GetExecutingAssembly()
                        .GetAllDependentAssemblies(it => it.Contains("SuperAPITest"))
                        .ToArray(); 

    apiObj!.DependencyInjectionOptions = new DependencyInjectionOptions(assemblyList);

    apiObj.InterfaceOptions.JsonSerializerSettings = new JsonSerializerSettings
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver()
    };

   //���ó���API
   api.EnableSuperApi(apiObj); 

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
