using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Scalar.AspNetCore;
using Store.API.Helper;
using Store.Data.Context;
using Store.Repository.Interfaces;
using Store.Repository.Repositories;
using Store.Service.Dtos.Products;
using Store.Service.Services.Products;
using Store.Service.Services.S3;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<StoreDbContext>(options =>
{
    options.UseNpgsql(connectionString);
    options.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
});
builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.Configure<SupabaseS3Options>(
    builder.Configuration.GetSection("Supabase:S3")
);
builder.Services.AddScoped<IStorageService, StorageService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

await ApplySeeding.ApplySeedingAsync(app, connectionString);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(o => { o.WithTitle("Store API"); });
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();