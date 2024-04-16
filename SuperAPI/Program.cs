using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Forms;
using ReZero;
using ReZero.SuperAPI;

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
                ConnectionString = "Server=.;Database=SuperAPI;User Id=sa;Password=sasa;",
                DbType = SqlSugar.DbType.SqlServer,
            },
        },
        AopOptions=new AopOptions()
        {
            DynamicApiAfterInvokeAsync=async context=>
            {
               //dynamic api jwt
               await Task.FromResult(string.Empty);
            },
            SystemApiAfterInvokeAsync = async context =>
            {
                //admin api jwt
                await Task.FromResult(string.Empty);
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
