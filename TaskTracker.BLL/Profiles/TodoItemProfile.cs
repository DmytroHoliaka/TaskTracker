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

        CreateMap<TodoItemDto, TodoItem>()
            .ForMember(
                model => model.Title,
                config => config.MapFrom(dto => $"Dto to model!!!: {dto.Title}"))
            .ConstructUsing(dto => new TodoItem(dto.Id, dto.Title, dto.State));
    }
}
