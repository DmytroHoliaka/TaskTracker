using FluentValidation;
using TaskTracker.BLL.TodoItems.Validators;

namespace TaskTracker.BLL.TodoItems.Commands.CreateTodoItem;

public class UpdateTodoItemCommandValidator 
    : TodoItemDtoValidator<CreateTodoItemCommand>
{
    public UpdateTodoItemCommandValidator()
    {
    }
}
