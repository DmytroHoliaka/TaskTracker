using AutoMapper;
using TaskTracker.BLL.Models.DbSet;
using TaskTracker.BLL.Models.Dtos;

namespace TaskTracker.BLL.Profiles;

public class TodoItemDtoProfile : Profile
{
    public TodoItemDtoProfile()
    {
        CreateMap<TodoItemDto, TodoItem>()
            .ForMember(
                model => model.Title,
                config => config.MapFrom(dto => $"Dto to model!!!: {dto.Title}"))
            .ConstructUsing(dto => new TodoItem(dto.Id, dto.Title, dto.State));
    }
}
