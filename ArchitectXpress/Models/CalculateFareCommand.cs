namespace ArchitectXpress.Models
{
    public class CalculateFareCommand
    {
        public Guid RideId { get; set; }
        public decimal DistanceInKm { get; set; }
        public int DurationInMinutes { get; set; }
        public decimal SurgeMultiplier { get; set; } = 1.0m;
    }

    public class FareCalculatedEvent
    {
        public Guid RideId { get; set; }
        public decimal TotalFare { get; set; }
    }
}
