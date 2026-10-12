
using Microsoft.AspNetCore.Mvc;
using TaskMasterAPI.Services;

namespace TaskMasterAPI.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TaskController : ControllerBase
{

    [HttpGet]
    public ActionResult<IEnumerable<Models.Task>> GetTask()
    {
        return Ok(TaskDataStore.Current.Tasks);
    }


    [HttpGet("{id}")]
    public ActionResult<Models.Task> GetTaskById(int id)
    {
        var task = TaskDataStore.Current.Tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            return NotFound("No se encontró la tarea");
        }

        return Ok(task);
    }


    [HttpPost]
    public ActionResult<Models.Task> CreateTask(Models.TaskInsert taskInsert)
    {

        var newTask = new Models.Task()
        {
            Id = TaskDataStore.Current.Tasks.Max(t => t.Id) + 1,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            IsCompleted = false,
            Title = taskInsert.Title,
            Description = taskInsert.Description
        };

        TaskDataStore.Current.Tasks.Add(newTask);

        return Ok(newTask);

    }


    [HttpPut("{id}")]
    public ActionResult<Models.Task> UpdateTask(int id, Models.TaskInsert taskUpdate)
    {
        var task = TaskDataStore.Current.Tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            return NotFound("No se encontró la tarea");
        }

        task.UpdatedAt = DateTime.Now;
        task.Title = taskUpdate.Title;
        task.Description = taskUpdate.Description;

        return Ok(task);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteTask(int id)
    {
        var task = TaskDataStore.Current.Tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            return NotFound("No se encontró la tarea");
        }

        TaskDataStore.Current.Tasks.Remove(task);

        return NoContent();
    }
}
