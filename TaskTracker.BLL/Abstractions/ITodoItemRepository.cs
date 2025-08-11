using TaskTracker.BLL.Models.DbSet;

namespace TaskTracker.BLL.Abstractions;

public interface ITodoItemRepository : IBaseRepository<TodoItem>
{
}
