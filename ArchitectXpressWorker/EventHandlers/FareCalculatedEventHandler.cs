using ArchitectXpressWorker.Models;
using MassTransit;
using System.Threading.Tasks;

namespace ArchitectXpressWorker.EventHandlers
{
 
    public class FareCalculatedEventHandler : IConsumer<FareCalculatedEvent>
    {
        public async Task Consume(ConsumeContext<FareCalculatedEvent> context)
        {
            var fareEvent = context.Message;

            // Update ride state with the calculated fare
            Console.WriteLine($"Fare received for RideId={fareEvent.RideId}, TotalFare={fareEvent.TotalFare}");

            // Perform additional business logic if needed
            await Task.CompletedTask;
        }
    }
}
