using Catalog.Application;
using Catalog.Infrustructure.Contexts;

var builder = WebApplication.CreateBuilder(args);

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

app.MapControllers();

await app.UseDatabaseSeeding();

app.Run();

