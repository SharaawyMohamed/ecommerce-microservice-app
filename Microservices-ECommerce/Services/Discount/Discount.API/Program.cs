using Basket.Infrustructure;
using Discount.API;
using Discount.API.Discount.Grpc.Services;
using Discount.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrustructure(builder.Configuration)
	.AddApplication()
	.AddDiscountAPI(builder.Configuration);
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

app.MapGet("/", async context =>
{
	await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client.");
});

app.Run();
