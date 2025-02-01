using ArchitectXpress.Models;
using ArchitectXpress.RabbitMQ;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectXpress.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PassengerController : ControllerBase
    {
        private readonly ILogger<PassengerController> _logger;
        private readonly IMessagePublisher _publisher;

        public PassengerController(
            ILogger<PassengerController> logger,
            IMessagePublisher publisher)
        {
            _logger = logger;
            _publisher = publisher;
        }

        [HttpPost(Name = "RegisterPassenger")]
        public async Task<string> RegisterPassengerAsync()
        {
            var data = new PassengerInfoEvent(
                "Koli " + Guid.NewGuid().ToString("D"), 25);
            await _publisher.PublishMessageAsync(data);
            return data.Name;
        }
    }
}
