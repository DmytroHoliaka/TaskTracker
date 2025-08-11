using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        // GET: api/TodoItems
        [HttpGet]
        public async Task<IActionResult> GetTodoItems(
            CancellationToken cancellationToken = default)
        {
            GetAllTodoItemsQuery query = new();
            Result<IEnumerable<TodoItemDto>> result = await Sender.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return Ok(result.Value);
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem(
            Guid id, 
            CancellationToken cancellationToken = default)
        {
            GetTodoItemByIdQuery query = new(TodoItemId: id);
            Result<TodoItemDto> result = await Sender.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return Ok(result.Value);
        }

        // PUT: api/TodoItems/5
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

            UpdateTodoItemCommand command = new(todoItemDto);
            Result<TodoItemDto> result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return Ok(result.Value);
        }

        // POST: api/TodoItems
        [HttpPost]
        public async Task<IActionResult> PostTodoItem(
            TodoItemDto todoItemDto, 
            CancellationToken cancellationToken = default)
        {
            CreateTodoItemCommand command = new(todoItemDto);
            Result<TodoItemDto> result = await Sender.Send(command, cancellationToken);
            
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return CreatedAtAction("GetTodoItem", new { id = result.Value.Id }, result.Value);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(
            Guid id, 
            CancellationToken cancellationToken = default)
        {
            DeleteTodoItemCommand command = new(id);
            Result<TodoItemDto> result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return NoContent();
        }
    }
}
