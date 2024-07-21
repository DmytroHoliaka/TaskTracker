using AutoMapper;
using TaskTracker.BLL.Models.DbSet;
using TaskTracker.BLL.Models.Dtos;

namespace TaskTracker.BLL.Profiles;

public class TodoItemProfile : Profile
{
    public TodoItemProfile()
    {
        CreateMap<TodoItem, TodoItemDto>()
            .ForMember(
                dto => dto.Title,
                config => config.MapFrom(model => $"Model to dto!!!: {model.Title}"))
            .ConstructUsing(model => new TodoItemDto(model.Id, model.Title, model.State));
    }
}
