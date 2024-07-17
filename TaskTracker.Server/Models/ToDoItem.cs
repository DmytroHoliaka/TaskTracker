namespace TaskTracker.Server.Models;

public class ToDoItem
{   
    // ToDo: Change using enum
    public int Id { get; set; }
    public string Title { get; set; }
    public string Status { get; set; } = "ToDo";
}