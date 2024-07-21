using MediatR;
using TaskTracker.BLL.Models.Dtos;
using TaskTracker.BLL.Shared;

namespace TaskTracker.BLL.TodoItems.Queries.GetTodoItemById;

public sealed record GetTodoItemByIdCommand (Guid TodoItemId) : IRequest<Result<TodoItemDto>>;
