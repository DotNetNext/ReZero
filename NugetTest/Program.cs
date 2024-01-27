using ReZero;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
//Register: Register the super API service
//×¢²á£º×¢²á³¬¼¶API·þÎñ
builder.Services.AddReZeroServices(new ReZeroOptions()
{
    SuperApiOptions = new ReZero.SuperAPI.SuperAPIOptions() 
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
