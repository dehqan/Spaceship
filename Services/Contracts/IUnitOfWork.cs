using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spaceship.Services.Contracts;

public interface IUnitOfWork : IDisposable
{
    IWeatherRepository WeatherRepository { get; }
    Task SaveAsync(CancellationToken cancellationToken);
}