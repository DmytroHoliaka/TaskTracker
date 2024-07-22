using MediatR;
using TaskTracker.BLL.Models.Dtos;
using TaskTracker.BLL.Shared;
using TaskTracker.BLL.TodoItems.Commands.General;

namespace TaskTracker.BLL.TodoItems.Commands.UpdateTodoItem;

public sealed record UpdateTodoItemCommand(TodoItemDto TodoItemDto) 
    : IRequest<Result<TodoItemDto>>, ITodoItemDtoCommand;
