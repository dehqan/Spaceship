using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spaceship.Infrastructure.Persistence.Entities;
using Spaceship.Services.Contracts;

namespace Spaceship.Infrastructure.Persistence.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly AppDbContext _dbContext;

        public WeatherRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<WeatherEntity> GetAsync(CancellationToken cancellationToken)
        {
            return _dbContext.Weathers.FirstOrDefaultAsync(cancellationToken);
        }

        public void Add(WeatherEntity weatherEntity)
        {
            _dbContext.Weathers.Add(weatherEntity);
        }

        public void Remove(WeatherEntity entity, CancellationToken cancellationToken)
        {
            _dbContext.Remove(entity);
        }
    }
}
