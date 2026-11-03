using Asp.Versioning.ApiExplorer;
using Basket.API;
using Basket.API.Configurations;
using Basket.API.Middlewares;
using Basket.Application;
using Basket.Infrustructure;
using Serilog;

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(Logging.Application.Logging.ConfigureLogging);

builder.Services
	.AddInfrustructure(builder.Configuration)
	.AddApplication(builder.Configuration)
	.AddAPIRegistration();

builder.Services.AddSwaggerGen(options =>
{
	options.CustomSchemaIds(type => type.FullName);
});

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();

	app.UseSwaggerUI(options =>
	{
		var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

		foreach (var description in provider.ApiVersionDescriptions)
		{
			options.SwaggerEndpoint(
				$"/swagger/{description.GroupName}/swagger.json",
				$"Basket API {description.GroupName.ToUpperInvariant()}");
		}
	});
}

app.UseMiddleware<ValidationExceptionMiddleware>();
app.MapControllers();
app.Run();