using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spaceship.Services.Contracts;

namespace Spaceship.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public ApiController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return Ok(await _weatherService.GetAsync(cancellationToken));
        }
    }
}