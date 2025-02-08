using System.Security;
using System.Threading;
using Archexpress.Demo.Passenger.Database;
using ArchitectXpress.Caching;
using ArchitectXpress.Common.Publisher;
using ArchitectXpress.Mongos;
using ArchitectXpressWorker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
namespace ArchitectXpress.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RideController : ControllerBase
    {
        private readonly ILogger<PassengerController> _logger;
        private readonly IMessagePublisher _publisher;
        private readonly ICacheService _cacheService;
        private readonly IMongoRepository<PassengerInfo> _passengerRepository;

        public RideController(
            ILogger<PassengerController> logger,
            IMessagePublisher publisher,
            ICacheService cacheService,
            IMongoRepository<PassengerInfo> passengerRepository)
        {
            _logger = logger;
            _publisher = publisher;
            _cacheService = cacheService;
            _passengerRepository = passengerRepository;
        }

        [HttpPost(Name = "Request")]
        public async Task<IActionResult> RideRequest(CalculateFareCommand data)
        {
            await _publisher.PublishMessageAsync(data);

            return Ok();
        }
    }
}
