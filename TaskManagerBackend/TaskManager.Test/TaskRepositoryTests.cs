using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using TaskManager.Data;
using TaskManager.Data.Repositories;

namespace TaskManager.Tests
{
    public class TaskRepositoryTests
    {
        private async Task<TasksDbContext> GetInMemoryDbContextAsync()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TasksDbContext(options);
            await context.Database.EnsureCreatedAsync();
            return context;
        }

        private TaskItem CreateTestTask(int id = 0) => new()
        {
            Id = id,
            Title = "Test Title",
            Description = "Test Description",
            DueDate = DateTime.Today.AddDays(3),
            Priority = "Medium",
            FullName = "John Smith",
            Telephone = "0501234567",
            Email = "john@example.com"
        };

        [Fact]
        public async Task CreateTaskAsync_Should_Add_Task()
        {
            var context = await GetInMemoryDbContextAsync();
            var repo = new TaskRepository(context);
            var task = CreateTestTask();

            var result = await repo.CreateTaskAsync(task);

            var tasksInDb = await context.Tasks.ToListAsync();
            tasksInDb.Should().ContainSingle();
            tasksInDb[0].Title.Should().Be("Test Title");
            result.Id.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetTask_Should_Return_Existing_Task()
        {
            var context = await GetInMemoryDbContextAsync();
            var task = CreateTestTask(1);
            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            var repo = new TaskRepository(context);
            var result = await repo.GetTask(1);

            result.Should().NotBeNull();
            result!.FullName.Should().Be("John Smith");
        }

        [Fact]
        public async Task GetAllTasksAsync_Should_Return_All_Tasks()
        {
            var context = await GetInMemoryDbContextAsync();
            context.Tasks.AddRange(CreateTestTask(1), CreateTestTask(2));
            await context.SaveChangesAsync();

            var repo = new TaskRepository(context);
            var result = await repo.GetAllTasksAsync();

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task UpdateTask_Should_Change_Task_Values()
        {
            var context = await GetInMemoryDbContextAsync();
            var original = CreateTestTask(1);
            context.Tasks.Add(original);
            await context.SaveChangesAsync();

            var updated = CreateTestTask(1);
            updated.Title = "Updated Title";
            updated.Priority = "High";

            var repo = new TaskRepository(context);
            await repo.UpdateTask(1, original, updated);

            var reloaded = await context.Tasks.FindAsync(1);
            reloaded!.Title.Should().Be("Updated Title");
            reloaded.Priority.Should().Be("High");
        }

        [Fact]
        public async Task DeleteTask_Should_Remove_Task()
        {
            var context = await GetInMemoryDbContextAsync();
            var task = CreateTestTask(1);
            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            var repo = new TaskRepository(context);
            await repo.DeleteTask(task);

            var tasks = await context.Tasks.ToListAsync();
            tasks.Should().BeEmpty();
        }
    }
}
