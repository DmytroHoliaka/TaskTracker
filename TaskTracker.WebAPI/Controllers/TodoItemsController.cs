using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.BLL.Models.DbSet;
using TaskTracker.BLL.Models.Dtos;
using TaskTracker.BLL.Shared;
using TaskTracker.BLL.TodoItems.Commands.CreateTodoItem;
using TaskTracker.BLL.TodoItems.Commands.DeleteTodoItem;
using TaskTracker.BLL.TodoItems.Commands.UpdateTodoItem;
using TaskTracker.BLL.TodoItems.Queries.GetAllTodoItems;
using TaskTracker.BLL.TodoItems.Queries.GetTodoItemById;
using TaskTracker.WebAPI.Abstractions;

namespace TaskTracker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController(ISender sender) : ApiController(sender)
    {
        // GET: api/ToDoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems(
            CancellationToken cancellationToken = default)
        {
            GetAllTodoItemsQuery query = new();
            Result<IEnumerable<TodoItemDto>> result = await Sender.Send(query, cancellationToken);

            return result switch
            {
                { IsSuccess: true } =>
                    Ok(result.Value),

                { Error.Code: TodoItemErrorCodes.DatabaseError } =>
                    StatusCode(StatusCodes.Status500InternalServerError, result.Error),

                _ =>
                    StatusCode(StatusCodes.Status500InternalServerError, TodoItemsErrors.UnexpectedError)
            };
        }

        // GET: api/ToDoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem(
            Guid id, 
            CancellationToken cancellationToken = default)
        {
            GetTodoItemByIdQuery query = new(TodoItemId: id);
            Result<TodoItemDto> result = await Sender.Send(query, cancellationToken);

            return result switch
            {
                { IsSuccess: true } =>
                    Ok(result.Value),

                { Error.Code: TodoItemErrorCodes.NoExists } =>
                    NotFound(result.Error),

                { Error.Code: TodoItemErrorCodes.DatabaseError } =>
                    StatusCode(StatusCodes.Status500InternalServerError, result.Error),

                _ =>
                    StatusCode(StatusCodes.Status500InternalServerError, TodoItemsErrors.UnexpectedError)
            };
        }

        // PUT: api/ToDoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(
            Guid id, 
            TodoItemDto todoItemDto, 
            CancellationToken cancellationToken = default)
        {
            if (id != todoItemDto.Id)
            {
                return BadRequest("Id in the URL doesn't match the Id in the payload.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UpdateTodoItemCommand command = new(todoItemDto);
            Result<TodoItemDto> result = await Sender.Send(command, cancellationToken);

            return result switch
            {
                { IsSuccess: true } =>
                    Ok(result.Value),

                { Error.Code: TodoItemErrorCodes.DatabaseError } =>
                    StatusCode(StatusCodes.Status500InternalServerError, result.Error),

                _ =>
                    StatusCode(StatusCodes.Status500InternalServerError, TodoItemsErrors.UnexpectedError)
            };
        }

        // POST: api/ToDoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(
            TodoItemDto todoItemDto, 
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // ToDo: Check does IsValid works correctly in ASP.NET Web API
            }

            CreateTodoItemCommand command = new(todoItemDto);
            Result<TodoItemDto> result = await Sender.Send(command, cancellationToken);

            return result switch
            {
                { IsSuccess: true } =>
                    CreatedAtAction("GetTodoItem", new { id = result.Value.Id }, result.Value),

                { Error.Code: TodoItemErrorCodes.DatabaseError } =>
                    StatusCode(StatusCodes.Status500InternalServerError, result.Error),

                _ =>
                    StatusCode(StatusCodes.Status500InternalServerError, TodoItemsErrors.UnexpectedError)
            };
        }

        // DELETE: api/ToDoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(
            Guid id, 
            CancellationToken cancellationToken = default)
        {
            DeleteTodoItemCommand command = new(id);
            Result<TodoItemDto> result = await Sender.Send(command, cancellationToken);

            return result switch
            {
                { IsSuccess: true } =>
                    NoContent(),

                { Error.Code: TodoItemErrorCodes.NoExists } =>
                    NotFound(result.Error),

                { Error.Code: TodoItemErrorCodes.DatabaseError } =>
                    StatusCode(StatusCodes.Status500InternalServerError, result.Error),

                _ =>
                    StatusCode(StatusCodes.Status500InternalServerError, TodoItemsErrors.UnexpectedError),
            };
        }
    }
}
