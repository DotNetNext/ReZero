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
//◊¢≤·£∫◊¢≤·≥¨º∂API∑˛ŒÒ
builder.Services.AddReZeroServices(api =>
{
    //∆Ù”√≥¨º∂API
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
        InterfaceOptions = new InterfaceOptions()
        {
            SuperApiAop = new JwtAop()//jwt¿πΩÿ∆˜
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
