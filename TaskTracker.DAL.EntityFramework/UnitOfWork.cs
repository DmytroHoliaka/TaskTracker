using Microsoft.EntityFrameworkCore.Storage;
using TaskTracker.BLL.Abstractions;
using TaskTracker.DAL.EntityFramework.Repositories;

namespace TaskTracker.DAL.EntityFramework;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly IDbContextTransaction _transaction;
    private readonly TaskTrackerContext _context;
    private TodoItemRepository? _todoItemRepository;

    public UnitOfWork(TaskTrackerContext context)
    {
        _context = context;
        _transaction = context.Database.BeginTransaction();
    }

    public ITodoItemRepository TodoItemRepository
        => _todoItemRepository ??= new TodoItemRepository(_context);

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);  // ToDo: Fix warning
        }
        catch
        {
            await RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        await _transaction.RollbackAsync(cancellationToken);
    }
    
    public void Dispose()
    {
        _transaction.Dispose();
    }
}
