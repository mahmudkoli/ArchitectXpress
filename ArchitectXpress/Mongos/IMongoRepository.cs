using System.Linq.Expressions;

namespace ArchitectXpress.Mongos;

public interface IMongoRepository<T>
    where T : class
{
    Task AddAsync(T entity);
    Task<long> CountAsync();
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllPagedAsync(int pageNumber, int pageSize);
    Task<IEnumerable<T>> GetByFilterAsync(Expression<Func<T, bool>> filter);
    Task<IEnumerable<T>> GetByFilterPagedAsync(Expression<Func<T, bool>> filter, int pageNumber, int pageSize);
}
