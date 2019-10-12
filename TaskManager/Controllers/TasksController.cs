using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Services;
using TaskManager.Services.Comunication;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService taskService;
        public TasksController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        // GET: api/Tasks
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TaskDTO>), 200)]
        public IActionResult GetAll()
        {
            var result = taskService.GetTasks();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // POST: api/Tasks
        [HttpPost]
        public IActionResult Post([FromBody] TaskDTO task)
        {
            var result = taskService.SaveTask(task);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TaskDTO task)
        {
            task.Id = id;
            var result = taskService.UpdateTask(task);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
