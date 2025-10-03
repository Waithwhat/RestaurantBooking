using System.Linq.Expressions;

namespace RestaurantBooking.Repositories;

public interface IRepository<T> where T : class
{
    Task<T?> GetAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task SaveChangesAsync();

    // Allows querying by predicate
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
}
