using TaskTracker.BLL.Models.Dtos;

namespace TaskTracker.BLL.TodoItems.Commands.General;

public interface ITodoItemDtoCommand
{
    public TodoItemDto TodoItemDto { get; }
}
