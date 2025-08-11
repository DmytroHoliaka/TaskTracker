using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskTracker.BLL.Abstractions;
using TaskTracker.BLL.Models.DbSet;
using TaskTracker.BLL.Models.Dtos;
using TaskTracker.BLL.Shared;

namespace TaskTracker.BLL.TodoItems.Commands.DeleteTodoItem;

public class DeleteTodoItemCommandHandler(
    ILogger<DeleteTodoItemCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<DeleteTodoItemCommand, Result<TodoItemDto>>
{
    public async Task<Result<TodoItemDto>> Handle(
        DeleteTodoItemCommand request, 
        CancellationToken cancellationToken)
    {
        try
        {
            TodoItem? itemToDelete = await unitOfWork.TodoItemRepository.GetByIdAsync(
                request.TodoItemId,
                cancellationToken);

            if (itemToDelete is null)
            {
                return Result.Failure<TodoItemDto>(TodoItemsErrors.NoExists);
            }

            await unitOfWork.TodoItemRepository.DeleteAsync(
                request.TodoItemId, 
                cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken);

            return Result.Success<TodoItemDto>(mapper.Map<TodoItemDto>(itemToDelete));
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex, 
                "An error occurred while deleting the todo item with ID {Id}.", 
                request.TodoItemId);

            return Result.Failure<TodoItemDto>(TodoItemsErrors.UnexpectedError);
        }
    }
}
