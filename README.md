# redis_aspnet_mvc_project

How to use server side Redis Cache in your ASP.Net Core application

First, ensure you have the necessary NuGet packages installed: StackExchange.Redis Microsoft.EntityFrameworkCore.SqlServer Microsoft.Extensions.Caching.StackExchangeRedis

This code assumes you have a DbContext named MyDbContext with a corresponding entity set up to access your SQL Server database. Replace YourEntity with your actual entity type. Adjust the caching duration (TimeSpan.FromMinutes(5)) according to your needs. Don't forget to register the services in your Startup.cs file:

public void ConfigureServices(IServiceCollection services){ services.AddDbContext(options => { options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")); });

services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = Configuration.GetConnectionString("RedisConnection");
});

services.AddSingleton<RedisCacheService>();
services.AddScoped<DataService>();

// Other service configurations...
}

Make sure to replace "DefaultConnection" and "RedisConnection" with your actual SQL Server and Redis connection strings respectively. With this setup, your data will be cached in Redis and retrieved from there if available, reducing the load on your SQL Server database.
