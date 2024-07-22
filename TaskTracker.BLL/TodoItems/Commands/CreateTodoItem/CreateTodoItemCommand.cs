using MediatR;
using TaskTracker.BLL.Models.Dtos;
using TaskTracker.BLL.Shared;

namespace TaskTracker.BLL.TodoItems.Commands.CreateTodoItem;

public sealed record CreateTodoItemCommand(TodoItemDto TodoItemDto) 
    : IRequest<Result<TodoItemDto>>;
