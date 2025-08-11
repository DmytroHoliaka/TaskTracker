namespace TaskTracker.BLL.Models.DbSet;

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