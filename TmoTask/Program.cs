using TmoTask.DataAccess;
using TmoTask.Interfaces;
using TmoTask.Logging;
using TmoTask.Middleware;
using TmoTask.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? logPath = builder.Configuration["LogFilePath"];
if (string.IsNullOrEmpty(logPath))
{
    throw new Exception("No Log file path found");
}

// Add services to the container.
builder.Services.AddSingleton<ILogger>(new FileLogger(logPath));
builder.Services.AddScoped<IDataHandler, DataHandler>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<ISellerService, SellerService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(x => x.WithOrigins("https://localhost:3000", "http://localhost:3000").AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
