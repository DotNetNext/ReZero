using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Forms;
using ReZero;
using ReZero.SuperAPI;
using SqlSugar;
using SuperAPITest;
using System.Reflection;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
//Register: Register the super API service
//ע�᣺ע�ᳬ��API����
builder.Services.AddReZeroServices(api =>
{
    //���ó���API
    api.EnableSuperApi(new SuperAPIOptions()
    {
        DatabaseOptions = new DatabaseOptions()
        {
            ConnectionConfig = new SuperAPIConnectionConfig()
            {
                ConnectionString = "server=.;uid=sa;pwd=sasa;database=SuperAPI",
                DbType = SqlSugar.DbType.SqlServer,
            },
        },
        UiOptions=new UiOptions 
        {
            //������Swagger��Ϊfalse
            ShowNativeApiDocument = true
        },
        InterfaceOptions = new InterfaceOptions()
        { 
            SuperApiAop = new JwtAop()//��Ȩ������
        },
        //����IOC: ע��IOC����Ҫ�����г��� 
        DependencyInjectionOptions=new DependencyInjectionOptions(Assembly
                                         .GetExecutingAssembly()
                                         //���������������ֻ�õ�һ�����
                                         .GetAllDependentAssemblies(it =>it.Contains("SuperAPITest")) 
                                         .ToArray())
    }); 

});

//ע��: ҵ������õ�SqlSugar���󣨷ǵʹ���ģʽ��
builder.Services.AddScoped<ISqlSugarClient>(it =>
{
    return new SqlSugarClient(new ConnectionConfig()
    {
        DbType = DbType.SqlServer,
        ConnectionString = "server=.;uid=sa;pwd=sasa;database=SuperAPI122",
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
