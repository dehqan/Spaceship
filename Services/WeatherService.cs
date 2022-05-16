using System;
using System.Threading;
using System.Threading.Tasks;
using Spaceship.Infrastructure.Persistence.Entities;
using Spaceship.Services.Contracts;

namespace Spaceship.Services;

public class WeatherService : IWeatherService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWeatherCachedService _weatherCachedService;
    private readonly IHttpClient _httpClient;

    private static readonly SemaphoreSlim Locker = new SemaphoreSlim(1, 1);


    public WeatherService(IUnitOfWork unitOfWork, IWeatherCachedService weatherCachedService, IHttpClient httpClient)
    {
        _unitOfWork = unitOfWork;
        _weatherCachedService = weatherCachedService;
        _httpClient = httpClient;
    }

    public async Task<string> GetAsync(CancellationToken cancellationToken)
    {
        bool success;
        var data = string.Empty;

        try
        {
            data = await _httpClient.GetAsync(cancellationToken);
            success = true;
        }
        catch (Exception)
        {
            success = false;
        }

        var weatherEntityInDb = await _weatherCachedService.GetCachedWeather(Resources.CacheKey.Weather, Locker,
            () => _unitOfWork.WeatherRepository.GetAsync(cancellationToken));

        if (!success) return weatherEntityInDb?.Data;

        try
        {
            // TODO BackgroundTask

            if (weatherEntityInDb is not null)
            {
                _unitOfWork.WeatherRepository.Remove(weatherEntityInDb, cancellationToken);
            }

            var weatherEntity = new WeatherEntity { Data = data };

            _unitOfWork.WeatherRepository.Add(weatherEntity);

            await _unitOfWork.SaveAsync(cancellationToken);

            await _weatherCachedService.SetCachedWeather(Resources.CacheKey.Weather, Locker, weatherEntity);
        }
        catch (Exception)
        {
            
        }
        

        return data;
    }
}