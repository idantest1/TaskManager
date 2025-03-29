using Microsoft.EntityFrameworkCore;

namespace TaskManager.Data
{
    public class TasksDbContext : DbContext
    {
        public TasksDbContext(DbContextOptions<TasksDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }
    }
}
