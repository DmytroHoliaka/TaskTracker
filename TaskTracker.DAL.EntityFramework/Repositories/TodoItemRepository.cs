using TaskTracker.BLL.Abstractions;
using TaskTracker.BLL.Models;
using TaskTracker.WebAPI.Controllers;

namespace TaskTracker.DAL.EntityFramework.Repositories;

public class TodoItemRepository(TaskTrackerContext context) : 
    BaseRepository<TodoItem>(context), ITodoItemRepository
{
}
