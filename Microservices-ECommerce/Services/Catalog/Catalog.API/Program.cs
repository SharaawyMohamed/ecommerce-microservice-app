using Catalog.Infrustructure.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrustructure(builder.Configuration);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();
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

await app.UseDatabaseSeeding();

app.Run();

