using Microsoft.AspNetCore.Components.Forms;
using ReZero;
using ReZero.SuperAPI;

var builder = WebApplication.CreateBuilder(args) ;
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Register: Register the super API service
//注册：注册超级API服务
builder.Services.AddReZeroServices(api => 
{
    //启用超级API
    api.EnableSuperApi(it => {
        //配置超级API的UI文件包（NUGET的真实路径）
        it.UiOptions.NugetPackagesPath =
         "C:\\Users\\Administrator\\.nuget\\packages";
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
