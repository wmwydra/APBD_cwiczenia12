using APBD_12.Data;
using APBD_12.Repositories;
using APBD_12.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IDbRepository, DbRepository>();
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IClientService, ClientService>();
//builder.Services.AddScoped<, DbRepository>();


builder.Services.AddDbContext<TripsDbContext>(opt =>
{
    var connectionString = builder.Configuration.GetConnectionString("Default");
    opt.UseSqlServer(connectionString);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();