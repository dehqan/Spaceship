using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Spaceship.Services.Contracts;

namespace Spaceship.Infrastructure.Http
{
    public class HttpClient : IHttpClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public HttpClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public async Task<string> GetAsync(CancellationToken cancellationToken)
        {
            var client = _clientFactory.CreateClient("weather");
            var result = await client.GetAsync(_configuration["WeatherServiceGuid"], cancellationToken);
            if (result.IsSuccessStatusCode) return await result.Content.ReadAsStringAsync(cancellationToken);
            else throw new Exception();
        }
    }
}
