using Microsoft.EntityFrameworkCore;
using TaskTracker.BLL.Models.DbSet;
using TaskTracker.DAL.EntityFramework;

namespace TaskTracker.WebAPI.Controllers;

public abstract class BaseRepository<T>(TaskTrackerContext context)
    where T : BaseModel, new()
{
    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public virtual Task UpdateAsync(T todoItem, CancellationToken cancellationToken = default)
    {
        context.Entry(todoItem).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public virtual Task CreateAsync(T todoItem, CancellationToken cancellationToken = default)
    {
        context.Entry(todoItem).State = EntityState.Added;
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        T taskToDelete = new()
        {
            Id = id,
        };

        context.Entry(taskToDelete).State = EntityState.Deleted;
        return Task.CompletedTask;
    }
}
