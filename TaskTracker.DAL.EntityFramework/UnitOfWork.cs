using Microsoft.EntityFrameworkCore.Storage;
using TaskTracker.BLL.Abstractions;
using TaskTracker.DAL.EntityFramework.Repositories;

namespace TaskTracker.DAL.EntityFramework;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private IDbContextTransaction _transaction;
    private TodoItemRepository _todoItemRepository;
    private TaskTrackerContext _context;

    public UnitOfWork(TaskTrackerContext context)
    {
        _context = context;
        _transaction = context.Database.BeginTransaction();
    }

    public ITodoItemRepository TodoItemRepository
        => _todoItemRepository ??= new TodoItemRepository(_context);

    public async Task CommitAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch
        {
            await RollbackAsync();
            throw;
        }
    }

    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
    }
    
    public void Dispose()
    {
        _transaction.Dispose();
    }
}
