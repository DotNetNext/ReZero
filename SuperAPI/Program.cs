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
//×¢²á£º×¢²á³¬¼¶API·þÎñ
builder.Services.AddReZeroServices(api =>
{
    //ÆôÓÃ³¬¼¶API
    api.EnableSuperApi(new SuperAPIOptions()
    {
        DatabaseOptions = new DatabaseOptions()
        {
            ConnectionConfig = new SuperAPIConnectionConfig()
            {
                ConnectionString = "server=.;uid=sa;pwd=sasa;database=SqlSugar5Dexxxxmo",
                DbType = SqlSugar.DbType.SqlServer,
            },
        },
        InterfaceOptions = new InterfaceOptions()
        { 
            SuperApiAop = new JwtAop()//ÊÚÈ¨À¹½ØÆ÷
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
