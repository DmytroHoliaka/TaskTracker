using Microsoft.EntityFrameworkCore;
using TaskTracker.BLL.Models.DbSet;

namespace TaskTracker.DAL.EntityFramework;

public class TaskTrackerContext : DbContext
{
    public DbSet<TodoItem> ToDoItems { get; set; } = default!;

    public TaskTrackerContext (DbContextOptions<TaskTrackerContext> options)
        : base(options)
    {
    }
}
