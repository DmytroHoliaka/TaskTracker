using FluentValidation;
using TaskTracker.BLL.TodoItems.Validators;

namespace TaskTracker.BLL.TodoItems.Commands.UpdateTodoItem;

public class UpdateTodoItemCommandValidator : TodoItemDtoValidator<UpdateTodoItemCommand>
{
    public UpdateTodoItemCommandValidator()
    {
    }
}
