using AutoMapper;
using FluentAssertions;
using Moq;
using TaskManager.Data;
using TaskManager.Data.Interfaces;
using TaskManager.Logic;
using TaskManager.Logic.Dto;
using TaskManager.Logic.Exceptions;

namespace TaskManager.Tests
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly TaskService _service;

        public TaskServiceTests()
        {
            _service = new TaskService(_repositoryMock.Object, _mapperMock.Object);
        }

        private TaskDto GetValidDto(int id = 0) => new()
        {
            Id = id,
            Title = "Test Task",
            Description = "Test description",
            DueDate = DateTime.Today.AddDays(3),
            Priority = "High",
            FullName = "John Doe",
            Telephone = "0501234567",
            Email = "john@example.com"
        };

        private TaskItem GetValidEntity(int id = 0) => new()
        {
            Id = id,
            Title = "Test Task",
            Description = "Test description",
            DueDate = DateTime.Today.AddDays(3),
            Priority = "High",
            FullName = "John Doe",
            Telephone = "0501234567",
            Email = "john@example.com"
        };

        [Fact]
        public async Task CreateTaskAsync_Should_Map_And_Save_Task()
        {
            // Arrange
            var dto = GetValidDto();
            var entity = GetValidEntity();

            _mapperMock.Setup(m => m.Map<TaskItem>(dto)).Returns(entity);
            _repositoryMock.Setup(r => r.CreateTaskAsync(entity));

            // Act
            var result = await _service.CreateTaskAsync(dto);

            // Assert
 
            _repositoryMock.Verify(r => r.CreateTaskAsync(entity), Times.Once);
        }

        [Fact]
        public async Task DeleteTask_Should_Delete_Existing_Task()
        {
            var entity = GetValidEntity(5);
            _repositoryMock.Setup(r => r.GetTask(5)).ReturnsAsync(entity);
            _repositoryMock.Setup(r => r.DeleteTask(entity)).Returns(Task.CompletedTask);

            await _service.DeleteTask(5);

            _repositoryMock.Verify(r => r.DeleteTask(entity), Times.Once);
        }

        [Fact]
        public async Task DeleteTask_Should_Throw_When_Task_Not_Found()
        {
            _repositoryMock.Setup(r => r.GetTask(999)).ReturnsAsync((TaskItem)null!);

            var act = () => _service.DeleteTask(999);

            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("*999*");
        }

        [Fact]
        public async Task GetAllTasksAsync_Should_Map_Results()
        {
            var items = new List<TaskItem> { GetValidEntity(1) };
            var dtos = new List<TaskDto> { GetValidDto(1) };

            _repositoryMock.Setup(r => r.GetAllTasksAsync()).ReturnsAsync(items);
            _mapperMock.Setup(m => m.Map<IEnumerable<TaskDto>>(items)).Returns(dtos);

            var result = await _service.GetAllTasksAsync();

            result.Should().HaveCount(1);
            result.First().Id.Should().Be(1);
        }

        [Fact]
        public async Task GetTask_Should_Return_Dto_When_Exists()
        {
            var entity = GetValidEntity(7);
            var dto = GetValidDto(7);

            _repositoryMock.Setup(r => r.GetTask(7)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<TaskDto>(entity)).Returns(dto);

            var result = await _service.GetTask(7);

            result.Id.Should().Be(7);
            result.Email.Should().Be("john@example.com");
        }

        [Fact]
        public async Task UpdateTask_Should_Map_And_Call_Repository()
        {
            var existing = GetValidEntity(8);
            var dto = GetValidDto();
            var mapped = GetValidEntity(8);

            _repositoryMock.Setup(r => r.GetTask(8)).ReturnsAsync(existing);
            _mapperMock.Setup(m => m.Map<TaskItem>(dto)).Returns(mapped);
            _repositoryMock.Setup(r => r.UpdateTask(8, existing, mapped)).Returns(Task.CompletedTask);

            await _service.UpdateTask(8, dto);

            _repositoryMock.Verify(r => r.UpdateTask(8, existing, It.Is<TaskItem>(t => t.Title == "Test Task")), Times.Once);
        }

        [Fact]
        public async Task UpdateTask_Should_Throw_When_NotFound()
        {
            _repositoryMock.Setup(r => r.GetTask(100)).ReturnsAsync((TaskItem)null!);
            var dto = GetValidDto();

            var act = () => _service.UpdateTask(100, dto);

            await act.Should().ThrowAsync<NotFoundException>();
        }
    }


}