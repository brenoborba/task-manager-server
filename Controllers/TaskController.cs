using Microsoft.AspNetCore.Mvc;
using TaskManagerServer.Models;
using TaskManagerServer.Repositories.Interfaces;

namespace TaskManagerServer.Controllers;
[Route("api/[controller]")]
[ApiController]

public class TaskController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;

    public TaskController(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<TaskModel>>> SearchAllTasks()
    {
        List<TaskModel> tasks = await _taskRepository.SearchAllTasks();
        return Ok(tasks);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskModel>> SearchTask(int id)
    {
        TaskModel task = await _taskRepository.SearchById(id);
        return Ok(task);
    }
    
    [HttpPost]
    public async Task<ActionResult<TaskModel>> Register([FromBody] TaskModel taskModel)
    {
        TaskModel task = await _taskRepository.Add(taskModel);
        return Ok(task);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TaskModel>> Refresh([FromBody] TaskModel taskModel, int id)
    {
        taskModel.Id = id;
        TaskModel task = await _taskRepository.Update(taskModel, id);
        return Ok(task);
    }
        
    [HttpDelete("{id}")]
    public async Task<ActionResult<TaskModel>> Delete([FromRoute] int id)
    {
        bool deleted = await _taskRepository.Delete(id);
        return Ok(deleted);
    }
}