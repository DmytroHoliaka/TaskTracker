using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskTracker.BLL.Abstractions;
using TaskTracker.BLL.Models.DbSet;
using TaskTracker.BLL.Models.Dtos;
using TaskTracker.BLL.Shared;

namespace TaskTracker.BLL.TodoItems.Queries.GetAllTodoItems;

public class GetAllTodoItemsCommandHandler(
    IUnitOfWork unitOfWork, 
    ILogger<GetAllTodoItemsCommandHandler> logger,
    IMapper mapper)
    : IRequestHandler<GetAllTodoItemsCommand, Result<IEnumerable<TodoItemDto>>>
{
    public async Task<Result<IEnumerable<TodoItemDto>>> Handle(GetAllTodoItemsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            IEnumerable<TodoItem> models = await unitOfWork.TodoItemRepository.GetAllAsync(cancellationToken);

            return Result.Success<IEnumerable<TodoItemDto>>(
                models.Select(model => mapper.Map<TodoItemDto>(model)));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving todo items.");
            return Result.Failure<IEnumerable<TodoItemDto>>(TodoItemsErrors.DatabaseError);
        }
    }
}
