using AutoMapper;
using TaskManager.Data;
using TaskManager.Logic.Dto;

namespace TaskManager.Logic.Mapper
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<TaskDto, TaskItem>().ReverseMap();
            CreateMap<IEnumerable<TaskDto>, IEnumerable<TaskItem>>();
        }
    }
}
