using Microsoft.EntityFrameworkCore;
using TaskManager.Data.Interfaces;

namespace TaskManager.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TasksDbContext _dbContext;

        public TaskRepository(TasksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TaskItem> CreateTaskAsync(TaskItem task)
        {
            _dbContext.Tasks.Add(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        public async Task DeleteTask(TaskItem task)
        {
            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            return await _dbContext.Tasks.ToListAsync();
        }

        public async Task<TaskItem?> GetTask(int id)
        {
            return await _dbContext.Tasks.FindAsync(id);
        }

        public async Task UpdateTask(int id, TaskItem existingTask, TaskItem task)
        {
                _dbContext.Entry(existingTask).CurrentValues.SetValues(task);
                _dbContext.Tasks.Attach(existingTask);
                _dbContext.Entry(existingTask).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
         }
    }
}
