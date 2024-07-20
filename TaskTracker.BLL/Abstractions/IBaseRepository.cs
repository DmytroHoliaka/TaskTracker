namespace TaskTracker.BLL.Abstractions;

public interface IBaseRepository<T>
    where T : class
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T?> GetByIdAsync(Guid id);

    Task UpdateAsync(T toDoItem);

    Task CreateAsync(T toDoItem);

    Task DeleteAsync(Guid id);
}
