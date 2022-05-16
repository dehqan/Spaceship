using System.Threading;
using System.Threading.Tasks;

namespace Spaceship.Services.Contracts
{
    public interface IWeatherService
    {
        Task<string> GetAsync(CancellationToken cancellationToken);
    }
}
