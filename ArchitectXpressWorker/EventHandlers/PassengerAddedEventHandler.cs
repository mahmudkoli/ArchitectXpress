using Archexpress.Demo.Passenger.Database;
using ArchitectXpress.Models;
using ArchitectXpressWorker.Mongos;
using MassTransit;

namespace ArchitectXpressWorker.EventHandlers;

public class PassengerAddedEventHandler : IConsumer<Passenger>
{
    private readonly IMongoRepository<PassengerInfo> _mongoRepository;

    public PassengerAddedEventHandler(IMongoRepository<PassengerInfo> mongoRepository)
    {
        _mongoRepository = mongoRepository;
    }

    public async Task Consume(ConsumeContext<Passenger> context)
    {
        var profile = context.Message;

        Console.WriteLine($"Passenger Added Consumer received: {profile.FirstName} {profile.LastName}");

        await _mongoRepository.AddAsync(
            new PassengerInfo(
                profile.Id.ToString(),
                $"{profile.FirstName} {profile.LastName}",
                new Random().Next(1, 9)));

        Console.WriteLine($"Successfully saved: {profile.FirstName} {profile.LastName}");

        await Task.CompletedTask;
    }
}