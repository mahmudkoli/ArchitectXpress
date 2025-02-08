using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectXpressWorker.Models
{
    public class CalculateFareCommand
    {
        public Guid RideId { get; set; }
        public decimal DistanceInKm { get; set; }
    }

    public class FareCalculatedEvent
    {
        public Guid RideId { get; set; }
        public decimal TotalFare { get; set; }
    }
}
