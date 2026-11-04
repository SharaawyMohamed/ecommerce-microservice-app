using Microsoft.AspNetCore.Components.Server;
using Ocelot.APIGatway;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
builder.Services.AddOcelotAPIGatwayConfigurations(builder.Configuration);
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoint =>
{
	endpoint.MapGet("/", async context =>
	{
		context.Response.WriteAsync("Welcome to Ocelot API Gateway");
	});
});
await app.UseOcelot();

await app.RunAsync();// because ocelot use asynchronous middleware, so we need to use await to run the application.
