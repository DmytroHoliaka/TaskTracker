using FluentValidation;
using MediatR;
using TaskTracker.BLL.Models.Dtos;
using TaskTracker.BLL.Shared;
using TaskTracker.BLL.TodoItems.Commands.General;

namespace TaskTracker.BLL.TodoItems.Validators;

public class TodoItemDtoValidator<TCommand> : AbstractValidator<TCommand>
    where TCommand : IRequest<Result>, ITodoItemDtoCommand
{
    public TodoItemDtoValidator()
    {
        RuleFor(command => command.TodoItemDto.Title).MaximumLength(64);
    }
}
