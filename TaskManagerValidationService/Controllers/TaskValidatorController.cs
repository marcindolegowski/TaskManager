using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerValidationService.Services;
using TaskManagerValidationService.Services.Comunication;

namespace TaskManagerValidationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskValidatorController : ControllerBase
    {
        private readonly ITaskValidationService taskValidationService;
        public TaskValidatorController(ITaskValidationService taskValidationService)
        {
            this.taskValidationService = taskValidationService;
        }

        [HttpGet("IsTaskNameUnique/{taskName}")]
        public IActionResult IsTaskNameUnique(string taskName)
        {
            Task.Delay(5000).Wait();
            var result = taskValidationService.IsTaskNameUnique(taskName);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
