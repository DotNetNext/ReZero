using ReZero;
using ReZero.SuperAPI;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Register: Register the super API service
//ע�᣺ע�ᳬ��API����
builder.Services.AddReZeroServices(api =>
{
    //���ó���API
    api.EnableSuperApi(new SuperAPIOptions()
    {
        UiOptions = new UiOptions { ShowNativeApiDocument = false }
    });

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

// ��������Ӧ�ó����ڱ���5000�˿�������  
string url = "http://localhost:5000/rezero/dynamic_interface.html?InterfaceCategoryId=200100";

// ����Ĭ�ϵ���ҳ���������ָ����URL  
Process.Start(new ProcessStartInfo
{
    FileName = url,
    UseShellExecute = true
});
app.Run();
