using Microsoft.EntityFrameworkCore;
using TaskTracker.BLL.Models;
using TaskTracker.DAL.EntityFramework;

namespace TaskTracker.WebAPI.Controllers;

public abstract class BaseRepository<T>(TaskTrackerContext context)
    where T : BaseModel, new()
{
    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await context.Set<T>().AsNoTracking().ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public virtual Task UpdateAsync(T todoItem)
    {
        context.Entry(todoItem).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public virtual Task CreateAsync(T todoItem)
    {
        context.Entry(todoItem).State = EntityState.Added;
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(Guid id)
    {
        T taskToDelete = new()
        {
            Id = id,
        };

        context.Entry(taskToDelete).State = EntityState.Deleted;
        return Task.CompletedTask;
    }
}
