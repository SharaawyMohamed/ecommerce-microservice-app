using Basket.Infrustructure;
using Discount.API;
using Discount.API.Discount.Grpc.Services;
using Discount.API.Middlewares;
using Discount.Application;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(Logging.Application.Logging.ConfigureLogging);

builder.Services.AddInfrustructure(builder.Configuration)
	.AddApplication()
	.AddDiscountAPI(builder.Configuration);

builder.WebHost.ConfigureKestrel(options =>
{
	options.ListenAnyIP(8080, listenOptions =>
	{
		listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
	});
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.MapGrpcService<DiscountService>();
app.UseMiddleware<ValidationExceptionMiddleware>();
app.MapGet("/", async context =>
{
	await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client.");
});

app.Run();
