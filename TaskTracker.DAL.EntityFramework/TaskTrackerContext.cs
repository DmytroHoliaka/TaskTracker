using Microsoft.EntityFrameworkCore;
using TaskTracker.BLL.Models;

namespace TaskTracker.DAL.EntityFramework;

public class TaskTrackerContext : DbContext
{
    public DbSet<TodoItem> ToDoItems { get; set; } = default!;

    public TaskTrackerContext (DbContextOptions<TaskTrackerContext> options)
        : base(options)
    {
    }
}
