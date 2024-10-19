using MetroDFS.API;
using MetroDFS.Presistance.Extensions;
using MetroDFS.Presistance.Seeding;
using MetroDFS.Services.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplicationServices()
    .AddPresistance(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDependencyInjectionServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
DataSeeding.Initialize(app.Services.CreateScope().ServiceProvider);
app.Run();
