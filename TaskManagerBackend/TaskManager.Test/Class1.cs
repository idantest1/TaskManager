using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManager.Logic.Dto;
using TaskManager.Logic.Interfaces;
using TaskManager.API.Controllers;

namespace TaskManager.Tests
{
    public class TasksControllerTests
    {
        private readonly Mock<ITaskService> _serviceMock = new();
        private readonly TasksController _controller;

        public TasksControllerTests()
        {
            _controller = new TasksController(_serviceMock.Object);
        }

        private TaskDto GetSampleDto(int id = 1) => new()
        {
            Id = id,
            Title = "Test Task",
            Description = "Test description",
            DueDate = System.DateTime.Today.AddDays(3),
            Priority = "High",
            FullName = "John Doe",
            Telephone = "0501234567",
            Email = "john@example.com"
        };

        [Fact]
        public async Task GetTasks_Should_Return_List_Of_Tasks()
        {
            var tasks = new List<TaskDto> { GetSampleDto(), GetSampleDto(2) };
            _serviceMock.Setup(s => s.GetAllTasksAsync()).ReturnsAsync(tasks);

            var result = await _controller.GetTasks();

            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            var value = okResult!.Value as IEnumerable<TaskDto>;
            value.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetTask_Should_Return_Task_If_Found()
        {
            var task = GetSampleDto();
            _serviceMock.Setup(s => s.GetTask(1)).ReturnsAsync(task);

            var result = await _controller.GetTask(1);

            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            var dto = okResult!.Value as TaskDto;
            dto!.Title.Should().Be("Test Task");
        }

        [Fact]
        public async Task GetTask_Should_Return_NotFound_If_Null()
        {
            _serviceMock.Setup(s => s.GetTask(99)).ReturnsAsync((TaskDto)null!);

            var result = await _controller.GetTask(99);

            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task CreateTask_Should_Return_Created_Task()
        {
            var task = GetSampleDto();
            _serviceMock.Setup(s => s.CreateTaskAsync(task)).ReturnsAsync(task);

            var result = await _controller.CreateTask(task);

            var created = result.Result as CreatedAtActionResult;
            created.Should().NotBeNull();
            created!.ActionName.Should().Be(nameof(TasksController.GetTask));
            (created.Value as TaskDto)!.Id.Should().Be(task.Id);
        }

        [Fact]
        public async Task UpdateTask_Should_Return_NoContent()
        {
            var task = GetSampleDto();
            _serviceMock.Setup(s => s.UpdateTask(1, task)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateTask(1, task);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteTask_Should_Return_NoContent()
        {
            _serviceMock.Setup(s => s.DeleteTask(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteTask(1);

            result.Should().BeOfType<NoContentResult>();
        }
    }
}
