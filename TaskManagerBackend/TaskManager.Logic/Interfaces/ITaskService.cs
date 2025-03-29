using TaskManager.Data;
using TaskManager.Logic.Dto;

namespace TaskManager.Logic.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasksAsync();
        Task<TaskDto> CreateTaskAsync(TaskDto task);
        Task<TaskDto> GetTask(int id);
        Task UpdateTask(int id, TaskDto task);
        Task DeleteTask(int id);
    }

}
