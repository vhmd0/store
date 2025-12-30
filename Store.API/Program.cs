using Microsoft.EntityFrameworkCore;
using Store.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<StoreDbContext>(options => options.UseNpgsql(connString));
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/openapi");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();