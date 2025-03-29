namespace TaskManager.Data.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllTasksAsync();
        Task<TaskItem> CreateTaskAsync(TaskItem task);
        Task<TaskItem?> GetTask(int id);
        Task UpdateTask(int id, TaskItem task, TaskItem entity);
        Task DeleteTask(TaskItem task);
    }
}
