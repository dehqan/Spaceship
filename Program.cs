using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spaceship.Infrastructure.Cache;
using Spaceship.Infrastructure.Persistence;
using Spaceship.Infrastructure.Persistence.Repositories;
using Spaceship.Services;
using Spaceship.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL_Connection"), b =>
    {
        b.MigrationsAssembly(typeof(Program).Assembly.GetName().Name);
        b.CommandTimeout(int.Parse(builder.Configuration["DatabaseTimeoutSeconds"]));
        b.EnableRetryOnFailure(0);
    });
});

builder.Services.AddMemoryCache();

builder.Services.AddScoped<ICacheProvider, CacheProvider>();
builder.Services.AddScoped<IHttpClient, Spaceship.Infrastructure.Http.HttpClient>();
builder.Services.AddScoped<IWeatherCachedService, WeatherCachedService>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddHttpClient("weather", client =>
{

    client.BaseAddress = new Uri("https://webhook.site/");
    client.DefaultRequestHeaders.Add("Accept", "text/html");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("Accept", "application/xml");
    client.DefaultRequestHeaders.Add("Accept", "application/xhtml+xml");

}).ConfigureHttpClient(client =>
{
    client.Timeout = TimeSpan.FromSeconds(int.Parse(builder.Configuration["HttpTimeoutSeconds"]));
});


builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

using (var scope = ((IApplicationBuilder) app).ApplicationServices.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}

app.Run();
