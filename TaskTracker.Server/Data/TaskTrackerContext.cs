using Microsoft.EntityFrameworkCore;
using TaskTracker.Server.Models;

namespace TaskTracker.Server.Data
{
    public class TaskTrackerContext : DbContext
    {
        public DbSet<TodoItem> ToDoItems { get; set; } = default!;

        public TaskTrackerContext (DbContextOptions<TaskTrackerContext> options)
            : base(options)
        {
        }
    }
}
