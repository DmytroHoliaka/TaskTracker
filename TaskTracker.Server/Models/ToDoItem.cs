using TaskTracker.Server.Service;

namespace TaskTracker.Server.Models;

// ToDo: Add validation in API for 0, 1 and 2 states
public class TodoItem
{   
    public Guid Id { get; set; }
    public string Title { get; set; }
    public States State { get; set; } = States.Todo;
}