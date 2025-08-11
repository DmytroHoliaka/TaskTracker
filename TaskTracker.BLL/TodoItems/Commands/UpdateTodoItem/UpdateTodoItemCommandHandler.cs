using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskTracker.BLL.Abstractions;
using TaskTracker.BLL.Models.DbSet;
using TaskTracker.BLL.Models.Dtos;
using TaskTracker.BLL.Shared;

namespace TaskTracker.BLL.TodoItems.Commands.UpdateTodoItem;

public class UpdateTodoItemCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILogger<UpdateTodoItemCommandHandler> logger) 
    : IRequestHandler<UpdateTodoItemCommand, Result<TodoItemDto>>
{
    public async Task<Result<TodoItemDto>> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await unitOfWork.TodoItemRepository.UpdateAsync(
                mapper.Map<TodoItem>(request.TodoItemDto), 
                cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken);
            return Result.Success<TodoItemDto>(request.TodoItemDto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while updating the todo item with Id {Id}.", request.TodoItemDto.Id);
            return Result.Failure<TodoItemDto>(TodoItemsErrors.DatabaseError);
        }
    }
} 
