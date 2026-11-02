using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Ordering.API.Middlewares;
using Ordering.Application;
using Ordering.Infrustructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(Logging.Application.Logging.ConfigureLogging);

builder.Services.AddInfrustructure(builder.Configuration)
	.AddApplication(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
var busControl = app.Services.GetRequiredService<IBusControl>();

Console.WriteLine($"MassTransit Bus: {busControl.Address}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseMiddleware<ValidationExceptionMiddleware>();
app.MapControllers();

app.Run();
