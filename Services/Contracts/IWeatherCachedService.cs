using System;
using System.Threading;
using System.Threading.Tasks;
using Spaceship.Infrastructure.Persistence.Entities;

namespace Spaceship.Services.Contracts
{
    public interface IWeatherCachedService
    {
        Task<WeatherEntity> GetCachedWeather(string cacheKey, SemaphoreSlim locker, Func<Task<WeatherEntity>> func);
        Task SetCachedWeather(string cacheKey, SemaphoreSlim locker, WeatherEntity weatherEntity);
    }
}
