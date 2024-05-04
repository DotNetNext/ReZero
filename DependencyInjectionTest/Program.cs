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
builder.Services.AddHttpContextAccessor();
//×¢²á£º×¢²á³¬¼¶API·þÎñ
builder.Services.AddReZeroServices(new ReZeroOptions()
{
    DependencyInjectionOptions = new DependencyInjectionOptions(typeof(Program).Assembly)
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
