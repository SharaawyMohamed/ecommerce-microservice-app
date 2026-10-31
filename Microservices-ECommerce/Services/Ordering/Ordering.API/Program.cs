using Ordering.Infrustructure;
using Ordering.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrustructure(builder.Configuration).AddApplication();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
