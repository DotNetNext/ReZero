using ReZero; 

var builder = WebApplication.CreateBuilder(args) ;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//×¢²á£ºÎÞ´úÂëAPI
builder.Services.AddReZeroServices(new ReZeroOptions() { 
 ConnectionConfig=new SqlSugar.ConnectionConfig() { 
   DbType=SqlSugar.DbType.SqlServer,
   ConnectionString= "server=.;uid=sa;pwd=sasa;database=SQLSUGAR4XTEST",
   IsAutoCloseConnection=true,
 }
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
