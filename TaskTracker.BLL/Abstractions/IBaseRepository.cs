namespace TaskTracker.BLL.Abstractions;

public interface IBaseRepository<T>
    where T : class
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task UpdateAsync(T toDoItem, CancellationToken cancellationToken = default);

    Task CreateAsync(T toDoItem, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
