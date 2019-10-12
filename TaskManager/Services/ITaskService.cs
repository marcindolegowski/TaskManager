using System.Collections.Generic;
using TaskManager.Model;
using TaskManager.Services.Comunication;

namespace TaskManager.Services
{
    public interface ITaskService
    {
        BaseResponse<IEnumerable<TaskDTO>> GetTasks();
        BaseResponse<TaskDTO> SaveTask(TaskDTO task);
        BaseResponse<TaskDTO> UpdateTask(TaskDTO task);
    }
}