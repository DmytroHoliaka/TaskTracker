using AutoMapper;
using TaskTracker.BLL.Models.DbSet;
using TaskTracker.BLL.Models.Dtos;

namespace TaskTracker.BLL.Profiles;

public class TodoItemProfile : Profile
{
    public TodoItemProfile()
    {
        CreateMap<TodoItem, TodoItemDto>()
            .ConstructUsing(model => new TodoItemDto(model.Id, model.Title, model.State))
            .ReverseMap()
            .ConstructUsing(dto => new TodoItem(dto.Id, dto.Title, dto.State));
    }
}
