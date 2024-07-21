using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mono.TextTemplating;
using TaskTracker.BLL.Abstractions;
using TaskTracker.BLL.Models;
using TaskTracker.BLL.Models.DbSet;
using TaskTracker.BLL.Models.Dtos;
using TaskTracker.BLL.Shared;
using TaskTracker.BLL.TodoItems.Queries.GetAllTodoItems;
using TaskTracker.BLL.TodoItems.Queries.GetTodoItemById;
using TaskTracker.WebAPI.Abstractions;

namespace TaskTracker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController(
        IUnitOfWork unitOfWork, 
        ILogger<TodoItemsController> logger, 
        ISender sender,
        IMapper mapper) 
        : ApiController(sender)
    {
        // GET: api/ToDoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems(CancellationToken cancellationToken = default)
        {
            GetAllTodoItemsCommand command = new();
            Result<IEnumerable<TodoItemDto>> result = await Sender.Send(command, cancellationToken);

            return result switch
            {
                { IsSuccess: true } => Ok(result.Value.Select(dto => mapper.Map<TodoItem>(dto))),
                { Error.Code: TodoItemErrorCodes.DatabaseError } => StatusCode(StatusCodes.Status500InternalServerError, result.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, TodoItemsErrors.UnexpectedError)
            };
        }

        // GET: api/ToDoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem(Guid id, CancellationToken cancellationToken)
        {
            GetTodoItemByIdCommand command = new(TodoItemId: id);
            Result<TodoItemDto> result = await Sender.Send(command, cancellationToken);

            return result switch
            {
                { IsSuccess: true } => Ok(result.Value),
                { Error.Code: TodoItemErrorCodes.NoExists } => NotFound(result.Error),
                { Error.Code: TodoItemErrorCodes.DatabaseError } => StatusCode(StatusCodes.Status500InternalServerError, result.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, TodoItemsErrors.UnexpectedError)
            };
        }

        // PUT: api/ToDoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(Guid id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest("Id in the URL doesn't match the Id in the payload.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await unitOfWork.TodoItemRepository.UpdateAsync(todoItem);
                await unitOfWork.CommitAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while updating the todo item with Id {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        // POST: api/ToDoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // ToDo: Check does IsValid works correctly in ASP.NET Web API
            }

            try
            {
                await unitOfWork.TodoItemRepository.CreateAsync(todoItem);
                await unitOfWork.CommitAsync();
                return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating a new todo item.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        // DELETE: api/ToDoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            try
            {
                TodoItem? itemToDelete = await unitOfWork.TodoItemRepository.GetByIdAsync(id);

                if (itemToDelete is null)
                {
                    return NotFound($"Todo item with Id {id} not found.");
                }

                await unitOfWork.TodoItemRepository.DeleteAsync(id);
                await unitOfWork.CommitAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while deleting the todo item with ID {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        // ToDo: Remove
        [HttpGet("test")]
        public IActionResult Test()
        {
            TodoItem originalModel = new(Guid.NewGuid(), "Incorrect mapping", States.Done);
            TodoItemDto originalDto = new(Guid.NewGuid(), "Incorrect mapping", States.Done);

            TodoItemDto dto = mapper.Map<TodoItemDto>(originalModel);
            logger.LogDebug("{Title}", dto.Title);

            TodoItem model = mapper.Map<TodoItem>(originalDto);
            logger.LogDebug("{Title}", model.Title);

            logger.LogError("Test error using Serilog");

            return Ok();
        }
    }
}
