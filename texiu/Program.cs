using System.Security.Cryptography.X509Certificates;
using texiu.Interface;
using texiu.Service;

var builder = WebApplication.CreateBuilder(args);

// Add custom configuraion for IConfiguration
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
	config.AddJsonFile("customsettings.json", optional: true, reloadOnChange: true);
});

// Add services to the container.
builder.Services.AddSingleton<IDataService, DataService>();
builder.Services.AddSingleton<IArrayService, ArrayService>();
builder.Services.AddSingleton<IRandomService, RandomService>();
builder.Services.AddSingleton<ITextService, TextService>();
builder.Services.AddMemoryCache();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Build app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

var wellcome = app.Configuration["wellcome"] ?? "Hello! Server is running.";
app.MapGet("/", () => wellcome);

app.Run();
