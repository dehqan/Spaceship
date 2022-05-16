using System.Threading;
using System.Threading.Tasks;
using Spaceship.Infrastructure.Persistence.Entities;

namespace Spaceship.Services.Contracts
{
    public interface IWeatherRepository
    {
        Task<WeatherEntity> GetAsync(CancellationToken cancellationToken);
        public void Add(WeatherEntity weatherEntity);
        public void Remove(WeatherEntity entity, CancellationToken cancellationToken);
    }
}
