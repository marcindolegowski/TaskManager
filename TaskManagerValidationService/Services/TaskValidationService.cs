using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerValidationService.Persistence.Repositories;
using TaskManagerValidationService.Services.Comunication;

namespace TaskManagerValidationService.Services
{
    public class TaskValidationService : ITaskValidationService
    {
        private readonly ITaskRepository taskRepository;
        public TaskValidationService(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        public BaseResponse IsTaskNameUnique(string taskName)
        {
            try
            {
                if (taskRepository.TaskExist(taskName))
                {
                    return new BaseResponse("Open task with this name already exists");
                }
                return new BaseResponse();
            }
            catch (Exception ex)
            {
                return new BaseResponse($"An error occured: {ex.Message}");
            }
        }
    }
}
