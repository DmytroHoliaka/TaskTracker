using Microsoft.EntityFrameworkCore;
using TaskTracker.WebAPI.Models;

namespace TaskTracker.WebAPI.Data
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
