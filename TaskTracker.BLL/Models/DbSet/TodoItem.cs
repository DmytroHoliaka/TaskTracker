namespace TaskTracker.BLL.Models.DbSet;

// ToDo: Add validation in API for 0, 1 and 2 states
public class TodoItem : BaseModel
{
    public string Title { get; set; }
    public States State { get; set; }

    public TodoItem()
        : base()
    {
        Title = string.Empty;
        State = States.Todo;
    }

    public TodoItem(Guid id, string title, States state)
        : base(id)
    {
        Title = title;
        State = state;
    }
}