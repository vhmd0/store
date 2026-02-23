using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Store.API.Helper;
using Store.Data.Context;
using Store.Repository.Interfaces;
using Store.Repository.Repositories;
using Store.Service.Middlewares;
using Store.Service.Services.CacheServices;
using Store.Service.Services.Products;
using Store.Service.Services.Products.Dtos;
using Store.Service.Services.S3;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgreSQL");
var redisConnectionString = builder.Configuration.GetConnectionString("Redis") ??
                            throw new InvalidOperationException("Connection string 'Redis' not found.");
builder.Services.Configure<SupabaseS3Options>(builder.Configuration.GetSection("Supabase:S3"));

builder.Services.AddDbContext<StoreDbContext>(options => { options.UseNpgsql(connectionString); });

builder.Services.AddSingleton<IConnectionMultiplexer>(opt =>
    ConnectionMultiplexer.Connect(redisConnectionString!));

builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = redisConnectionString; });

builder.Services.AddHybridCache();
builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<ICacheServices, CacheServices>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IStorageService, StorageService>();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

// Add the Swagger generation service
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

await ApplySeeding.ApplySeedingAsync(app, connectionString);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
    app.UseSwagger();
    // Enable the Swagger UI middleware
    app.UseSwaggerUI(options =>
    {
        // Specify the endpoint for the OpenAPI JSON document
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    });
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();