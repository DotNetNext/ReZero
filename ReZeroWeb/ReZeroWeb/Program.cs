using ReZero; 

var builder = WebApplication.CreateBuilder(args) ;
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//ע�᣺�޴���API  �������� /rezero ���Է���
builder.Services.AddReZeroServices();




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
