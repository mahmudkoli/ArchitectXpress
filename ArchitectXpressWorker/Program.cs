using ArchitectXpressWorker;
using ArchitectXpressWorker.Mongos;
using ArchitectXpressWorker.RabbitMQ;
using MongoDB.Bson.Serialization.Conventions;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection(nameof(RabbitMQSettings)));
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection(nameof(MongoDBSettings)));
builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

var conventionPack = new ConventionPack
{
    new CamelCaseElementNameConvention()
};
ConventionRegistry.Register("CamelCase", conventionPack, t => true);

builder.Services.MassTransitConfig(builder.Configuration);

var host = builder.Build();
host.Run();
