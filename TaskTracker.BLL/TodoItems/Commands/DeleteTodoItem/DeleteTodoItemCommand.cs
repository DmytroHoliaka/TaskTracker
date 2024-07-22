using MediatR;
using TaskTracker.BLL.Models.Dtos;
using TaskTracker.BLL.Shared;

namespace TaskTracker.BLL.TodoItems.Commands.DeleteTodoItem;

public sealed record DeleteTodoItemCommand(Guid TodoItemId) : IRequest<Result<TodoItemDto>>;