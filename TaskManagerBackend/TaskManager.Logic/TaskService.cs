using AutoMapper;
using TaskManager.Data;
using TaskManager.Data.Interfaces;
using TaskManager.Logic.Dto;
using TaskManager.Logic.Exceptions;
using TaskManager.Logic.Interfaces;

namespace TaskManager.Logic
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository,IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<TaskDto> CreateTaskAsync(TaskDto taskDto)
        {
            var task = _mapper.Map<TaskItem>(taskDto);
            await  _taskRepository.CreateTaskAsync(task);
            taskDto.Id = task.Id;
            return taskDto;
        }

        public async Task DeleteTask(int id)
        {
            var existing = await _taskRepository.GetTask(id) ?? throw new NotFoundException(id);
            await _taskRepository.DeleteTask(existing);
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
          return  _mapper.Map<IEnumerable<TaskDto>>(await _taskRepository.GetAllTasksAsync());
        }

        public async Task<TaskDto> GetTask(int id)
        {
            var existing = await _taskRepository.GetTask(id);
            return existing == null ? throw new NotFoundException(id) : _mapper.Map<TaskDto>(existing);
        }

        public async Task UpdateTask(int id, TaskDto taskDto)
        {
            var existing = await _taskRepository.GetTask(id) ?? throw new NotFoundException(id);
            var entity = _mapper.Map<TaskItem>(taskDto);
            entity.Id = id;

            await _taskRepository.UpdateTask(id, existing,entity);
        }
    }
}
