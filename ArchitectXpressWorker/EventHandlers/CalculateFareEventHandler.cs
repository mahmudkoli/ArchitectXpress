using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectXpressWorker.EventHandlers
{
    using ArchitectXpressWorker.Models;
    using MassTransit;
    using System.Threading.Tasks;

    public class CalculateFareEventHandler : IConsumer<CalculateFareCommand>
    {
        public async Task Consume(ConsumeContext<CalculateFareCommand> context)
        {
            var command = context.Message;

            // Fare calculation logic
            decimal totalFare = CalculateFare(command.DistanceInKm, command.DurationInMinutes, command.SurgeMultiplier);

            // Publish FareCalculatedEvent
            await context.Publish(new FareCalculatedEvent
            {
                RideId = command.RideId,
                TotalFare = totalFare
            });

            Console.WriteLine($"Fare calculated and event published for RideId={command.RideId}, TotalFare={totalFare}");
        }

        private decimal CalculateFare(decimal distanceInKm, int durationInMinutes, decimal surgeMultiplier)
        {
            const decimal BaseFare = 2.50m;
            const decimal CostPerKm = 1.25m;
            const decimal CostPerMinute = 0.20m;
            const decimal ServiceFee = 1.00m;
            const decimal MinimumFare = 5.00m;

            decimal distanceCost = distanceInKm * CostPerKm;
            decimal timeCost = durationInMinutes * CostPerMinute;
            decimal totalFare = (BaseFare + distanceCost + timeCost + ServiceFee) * surgeMultiplier;

            return Math.Max(totalFare, MinimumFare);
        }
    }

}
