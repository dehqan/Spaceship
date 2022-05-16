using System.Threading;
using System.Threading.Tasks;

namespace Spaceship.Services.Contracts
{
    public interface IHttpClient
    {
        Task<string> GetAsync(CancellationToken cancellationToken);
    }
}
