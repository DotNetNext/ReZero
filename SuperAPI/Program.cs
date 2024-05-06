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
//注册：注册超级API服务
builder.Services.AddReZeroServices(api =>
{
    //启用超级API
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
            //不加载Swagger设为false
            ShowNativeApiDocument = true
        },
        InterfaceOptions = new InterfaceOptions()
        { 
            SuperApiAop = new JwtAop()//授权拦截器
        },
        //启用IOC: 注入IOC所需要的所有程序集 
        DependencyInjectionOptions=new DependencyInjectionOptions(Assembly
                                         .GetExecutingAssembly()
                                         //根据条件过滤这儿只用到一个类库
                                         .GetAllDependentAssemblies(it =>it.Contains("SuperAPITest")) 
                                         .ToArray())
    }); 

});

//注册: 业务代码用的SqlSugar对象（非低代码模式）
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
