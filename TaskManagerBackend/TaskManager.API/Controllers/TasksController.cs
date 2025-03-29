using Microsoft.AspNetCore.Mvc;
using TaskManager.Logic.Dto;
using TaskManager.Logic.Interfaces;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks()
        {
            return Ok(await _taskService.GetAllTasksAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTask(int id)
        {
            var task = await _taskService.GetTask(id);
            if (task == null)
                return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskDto>> CreateTask([FromBody] TaskDto task)
        {
            var created = await _taskService.CreateTaskAsync(task);
            return CreatedAtAction(nameof(GetTask), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskDto task)
        {
            await _taskService.UpdateTask(id, task);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
        
            await _taskService.DeleteTask(id);
            return NoContent();
        }
    }
}
