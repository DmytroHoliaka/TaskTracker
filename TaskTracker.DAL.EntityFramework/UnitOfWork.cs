using TaskTracker.BLL.Abstractions;
using TaskTracker.BLL.Exceptions;
using TaskTracker.DAL.EntityFramework.Repositories;

namespace TaskTracker.DAL.EntityFramework;

public class UnitOfWork(TaskTrackerContext context) : IUnitOfWork
{
    private TodoItemRepository? _todoItemRepository;

    public ITodoItemRepository TodoItemRepository
        => _todoItemRepository ??= new TodoItemRepository(context);

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new DatabaseOperationException("An unexpected error happend while working with database", ex);
        }
    }
}
