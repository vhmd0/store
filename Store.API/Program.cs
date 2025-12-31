using Microsoft.EntityFrameworkCore;
using Store.API.Helper;
using Store.Data.Context;
using Store.Repository.Interfaces;
using Store.Repository.Repositories;
using Store.Service.Services.Product;
using Store.Service.Services.Product.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<StoreDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddAutoMapper(option => { option.AddMaps(typeof(ProductProfile)); });

builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// 
var app = builder.Build();

await ApplySeeding.ApplySeedingAsync(app, connectionString);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/openapi");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();