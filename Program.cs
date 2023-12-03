using System.Reflection.Metadata;
using events_back.Context;
using events_back.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var stringConnection = Environment.GetEnvironmentVariable("EVENT_STRING_CONNETCION");

builder.Services.AddDbContext<EventContext>(options => 
    options.UseSqlServer("Data Source=192.168.100.93,1433;Initial Catalog=event;User ID=sa;Password=YourStrongPassword!;MultipleActiveResultSets=True;TrustServerCertificate=true"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
