using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace ArchitectXpressWorker.Mongos;

public class MongoRepository<T> : IMongoRepository<T>
    where T : class
{
    private readonly IMongoCollection<T> _collection;

    public MongoRepository(IOptions<MongoDBSettings> settings)
    {
        string connectionString = settings.Value.ConnectionString;
        string databaseName = settings.Value.DatabaseName;
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<T>(typeof(T).Name.ToLower() + "s");
    }

    public async Task AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var result = await _collection.FindAsync(Builders<T>.Filter.Empty);
        return await result.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        var result = await _collection.FindAsync(
            Builders<T>.Filter.Empty,
            new FindOptions<T> { Limit = pageSize, Skip = (pageNumber - 1) * pageSize });
        return await result.ToListAsync();
    }

    public async Task<long> CountAsync()
    {
        long count = await _collection.CountDocumentsAsync(Builders<T>.Filter.Empty);
        return count;
    }

    public async Task<IEnumerable<T>> GetByFilterAsync(Expression<Func<T, bool>> filter)
    {
        var result = await _collection.FindAsync(filter);
        return await result.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetByFilterPagedAsync(Expression<Func<T, bool>> filter, int pageNumber, int pageSize)
    {
        var result = await _collection.FindAsync(
            filter,
            new FindOptions<T> { Limit = pageSize, Skip = (pageNumber - 1) * pageSize });
        return await result.ToListAsync();
    }
}