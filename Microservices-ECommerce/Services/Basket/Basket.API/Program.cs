using Basket.API.Middlewares;
using Basket.Application;
using Basket.Infrustructure;
using Serilog;

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(Logging.Application.Logging.ConfigureLogging);
// Add services to the container.
builder.Services.AddInfrustructure(builder.Configuration)
	.AddApplication(builder.Configuration);

builder.Services.AddControllers();
// Configure Swagger
builder.Services.AddSwaggerGen();
// lightweight OpenAPI helper
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
app.UseMiddleware<ValidationExceptionMiddleware>();
app.MapControllers();

app.Run();
