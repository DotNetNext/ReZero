using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Forms;
using ReZero;
using ReZero.SuperAPI;
using SuperAPITest;

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
        InterfaceOptions = new InterfaceOptions()
        {
            //AuthorizationLocalStorageName说明：
            //localStorage["jwt"]="token";
            //如果localStorage["jwt"]有token那么本地html页面下接口请求都会带上token
            //一般用于本地调试用
            AuthorizationLocalStorageName = "jwt",
            SuperApiAop = new JwtAop()//授权拦截器
        }
    }); ;

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
