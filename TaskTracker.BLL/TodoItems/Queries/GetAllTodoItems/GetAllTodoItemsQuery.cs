using MediatR;
using TaskTracker.BLL.Models.Dtos;
using TaskTracker.BLL.Shared;

namespace TaskTracker.BLL.TodoItems.Queries.GetAllTodoItems;

public sealed record GetAllTodoItemsQuery() : IRequest<Result<IEnumerable<TodoItemDto>>>;
