namespace TaskTracker.BLL.Abstractions;

public interface IUnitOfWork
{
    ITodoItemRepository TodoItemRepository { get; }

    Task CommitAsync();
    Task RollbackAsync();
}
