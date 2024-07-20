namespace TaskTracker.BLL.Models;

// ToDo: Add validation in API for 0, 1 and 2 states
public class TodoItem : BaseModel
{   
    public string Title { get; set; } = default!;
    public States State { get; set; } = States.Todo;
}