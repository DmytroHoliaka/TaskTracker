using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskTracker.BLL.Abstractions;
using TaskTracker.BLL.Models.DbSet;
using TaskTracker.BLL.Models.Dtos;
using TaskTracker.BLL.Shared;

namespace TaskTracker.BLL.TodoItems.Queries.GetTodoItemById;

public class GetTodoItemByIdCommandHandler(
    IUnitOfWork unitOfWork, 
    ILogger<GetTodoItemByIdCommandHandler> logger,
    IMapper mapper) 
    : IRequestHandler<GetTodoItemByIdCommand, Result<TodoItemDto>>
{
    public async Task<Result<TodoItemDto>> Handle(
        GetTodoItemByIdCommand request, 
        CancellationToken cancellationToken)
    {
		try
		{
            TodoItem? item = await unitOfWork.TodoItemRepository.GetByIdAsync(request.TodoItemId, cancellationToken);

            if (item is null)
            {
                return Result.Failure<TodoItemDto>(TodoItemsErrors.NoExists);
            }

            return Result.Success<TodoItemDto>(mapper.Map<TodoItemDto>(item));
        }
		catch (Exception ex)
		{
            logger.LogError(ex, "An error occurred while retrieving the todo item with Id {Id}.", request.TodoItemId);
            return Result.Failure<TodoItemDto>(TodoItemsErrors.DatabaseError);
		}
    }
}
