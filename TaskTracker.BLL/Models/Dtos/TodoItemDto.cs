using System.ComponentModel.DataAnnotations;

namespace TaskTracker.BLL.Models.Dtos;

public class TodoItemDto
{
    public Guid Id { get; init; }
    public string Title { get; set; }
    public States State { get; set; }

    public TodoItemDto()
    {
        Id = Guid.NewGuid();
        Title = string.Empty;
        State = States.Todo;
    }

    public TodoItemDto(Guid id, string title, States state)
    {
        Id = id;
        Title = title;
        State = state;
    }
}
