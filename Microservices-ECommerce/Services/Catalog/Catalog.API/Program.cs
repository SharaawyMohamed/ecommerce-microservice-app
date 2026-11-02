using Catalog.API.Middlewares;
using Catalog.Application;
using Catalog.Infrustructure.Contexts;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(Logging.Application.Logging.ConfigureLogging);

builder.Services.AddInfrustructure(builder.Configuration);
builder.Services.AddApplication();


builder.Services.AddControllers();
 
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseMiddleware<ValidationExceptionMiddleware>();

app.MapControllers();

await app.UseDatabaseSeeding();

app.Run();

