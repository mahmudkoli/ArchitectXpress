using System.Security;
using System.Threading;
using Archexpress.Demo.Passenger.Database;
using ArchitectXpress.Caching;
using ArchitectXpress.Common.Publisher;
using ArchitectXpress.Mongos;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectXpress.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PassengerController : ControllerBase
    {
        private readonly ILogger<PassengerController> _logger;
        private readonly IMessagePublisher _publisher;
        private readonly ICacheService _cacheService;
        private readonly IMongoRepository<PassengerInfo> _passengerRepository;

        public PassengerController(
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

        [HttpGet(Name = "GetPassengers")]
        public async Task<List<PassengerInfo>> GetPassengersAsync(CancellationToken cancellationToken)
        {
            var passengers = await _cacheService.GetOrSetAsync(
            "passengers",
            () => _passengerRepository.GetAllAsync(),
            cancellationToken: cancellationToken);

            return passengers?.ToList() ?? [];
        }

        [HttpPost(Name = "RegisterPassenger")]
        public async Task<string> RegisterPassengerAsync(Passenger data)
        {
            await _publisher.PublishMessageAsync(data);
            return data.Id;
        }
    }
}
