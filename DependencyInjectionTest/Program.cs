using DependencyInjectionTest.Controllers;
using ReZero;
using ReZero.DependencyInjection;
using System.Runtime.CompilerServices;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddScoped(typeof(IClass1),typeof(Class1));
//Register: Register the super API service
//注册：注册超级API服务
builder.Services.AddReZeroServices(api =>
{ 
    //启用IOC
   api.EnableDependencyInjection(typeof(Program).Assembly);

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
