using ReZero;
using ReZero.SuperAPI;
using System.Diagnostics;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder =>
    {
        builder.AllowAnyOrigin() // �����κ���Դ  
               .AllowAnyHeader() // �����κ�ͷ  
               .AllowAnyMethod(); // �����κη�������GET, POST, PUT��  
    });
});

//Register: Register the super API service
//ע�᣺ע�ᳬ��API����
builder.Services.AddReZeroServices(api =>
{
    //���ó���API
    //�����ؿɻ�json�ļ�
    var apiObj = SuperAPIOptions.GetOptions();
     
    //���ó���API
    api.EnableSuperApi(apiObj);

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyCorsPolicy"); // ʹ�ö���Ŀ������  

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#if !DEBUG 
try 
	{	        
		    // ��������Ӧ�ó����ڱ���5000�˿�������  
            string url = "http://localhost:5000/rezero/dynamic_interface.html?InterfaceCategoryId=200100";
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            }); 
	}
	catch (global::System.Exception)
	{
     //docker�в��ܴ����������������
	}
#endif
// ����Ĭ�ϵ���ҳ���������ָ����URL  

app.Run();
