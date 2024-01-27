using ReZero;
using ReZero.SuperAPI;

var builder = WebApplication.CreateBuilder(args) ;
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Register: Register the super API service
//×¢²á£º×¢²á³¬¼¶API·þÎñ
builder.Services.AddReZeroServices(it => 
{
    it.SuperApiOptions!.UiOptions!.NugetPackagesPath=
    "C:\\Users\\Administrator\\.nuget\\packages";
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
