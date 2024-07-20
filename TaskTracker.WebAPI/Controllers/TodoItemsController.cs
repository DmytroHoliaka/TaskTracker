using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TaskTracker.BLL.Abstractions;
using TaskTracker.BLL.Models;

namespace TaskTracker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController(IUnitOfWork unitOfWork, ILogger<TodoItemsController> logger) : ControllerBase
    {
        // GET: api/ToDoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            try
            {
                IEnumerable<TodoItem> items = await unitOfWork.TodoItemRepository.GetAllAsync();
                return Ok(items);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving todo items.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        // GET: api/ToDoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(Guid id)
        {
            try
            {
                TodoItem? item = await unitOfWork.TodoItemRepository.GetByIdAsync(id);

                if (item is null)
                {
                    return NotFound($"Todo item with Id {id} not found.");
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving the todo item with Id {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
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
    }
}
