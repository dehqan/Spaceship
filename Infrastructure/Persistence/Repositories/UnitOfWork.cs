using System;
using System.Threading;
using System.Threading.Tasks;
using Spaceship.Services.Contracts;

namespace Spaceship.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{

    private readonly AppDbContext _appDbContext;
    private bool _disposed;

    public IWeatherRepository WeatherRepository => new WeatherRepository(_appDbContext);

    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _appDbContext.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}