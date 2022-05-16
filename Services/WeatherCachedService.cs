using System;
using System.Threading;
using System.Threading.Tasks;
using Spaceship.Infrastructure.Persistence.Entities;
using Spaceship.Services.Contracts;

namespace Spaceship.Services
{
    public class WeatherCachedService : IWeatherCachedService

    {
        private readonly ICacheProvider _cacheProvider;

        public WeatherCachedService(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public async Task<WeatherEntity> GetCachedWeather(string cacheKey, SemaphoreSlim locker, Func<Task<WeatherEntity>> func)
        {
            var weatherEntity = _cacheProvider.GetFromCache<WeatherEntity>(cacheKey);
            if (weatherEntity is not null) return weatherEntity;
            try
            {
                await locker.WaitAsync();

                weatherEntity = _cacheProvider.GetFromCache<WeatherEntity>(cacheKey);
                if (weatherEntity is not null) return weatherEntity;

                weatherEntity = await func();

                if (weatherEntity is not null) _cacheProvider.SetCache(cacheKey, weatherEntity);
            }
            finally
            {
                locker.Release();
            }

            return weatherEntity;
        }

        public async Task SetCachedWeather(string cacheKey, SemaphoreSlim locker, WeatherEntity weatherEntity)
        {
            try
            {
                await locker.WaitAsync();

                if (weatherEntity is not null) _cacheProvider.SetCache(cacheKey, weatherEntity);
            }
            finally
            {
                locker.Release();
            }
        }
    }
}
