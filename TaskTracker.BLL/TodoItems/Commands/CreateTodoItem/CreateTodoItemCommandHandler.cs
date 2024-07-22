using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskTracker.BLL.Abstractions;
using TaskTracker.BLL.Models.DbSet;
using TaskTracker.BLL.Models.Dtos;
using TaskTracker.BLL.Shared;

namespace TaskTracker.BLL.TodoItems.Commands.CreateTodoItem;

public class CreateTodoItemCommandHandler(
    ILogger<CreateTodoItemCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IMapper mapper
    )
    : IRequestHandler<CreateTodoItemCommand, Result<TodoItemDto>>
{
    public async Task<Result<TodoItemDto>> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await unitOfWork.TodoItemRepository.CreateAsync(
                mapper.Map<TodoItem>(request.TodoItemDto), 
                cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken);
            return Result.Success<TodoItemDto>(request.TodoItemDto);
        }   
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while creating a new todo item.");
            return Result.Failure<TodoItemDto>(TodoItemsErrors.DatabaseError);
        }
    }
}
