using ArchitectXpress.Caching;
using ArchitectXpress.Mongos;
using ArchitectXpress.RabbitMQ;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Conventions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. services
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection(nameof(MongoDBSettings)));
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection(nameof(RabbitMQSettings)));
builder.Services.Configure<CachingOptions>(builder.Configuration.GetSection(nameof(CachingOptions)));
builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
builder.Services.AddTransient<ICacheService, CacheService>();
builder.Services.AddHttpContextAccessor();

var conventionPack = new ConventionPack
{
    new CamelCaseElementNameConvention()
};
ConventionRegistry.Register("CamelCase", conventionPack, t => true);

builder.Services.AddStackExchangeRedisCache(options =>
{
    var cacheOptions = builder.Configuration.GetSection(nameof(CachingOptions)).Get<CachingOptions>();

    options.Configuration = cacheOptions.RedisURL;
    options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
    {
        AbortOnConnectFail = true,
        EndPoints = { cacheOptions.RedisURL },
        User = cacheOptions.Username,
        Password = cacheOptions.Password
    };
});

builder.Services.MassTransitConfig(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
