using TaskTracker.WebAPI.Service;

namespace TaskTracker.WebAPI.Models;

// ToDo: Add validation in API for 0, 1 and 2 states
public class TodoItem
{   
    public Guid Id { get; set; }
    public string Title { get; set; }
    public States State { get; set; } = States.Todo;
}