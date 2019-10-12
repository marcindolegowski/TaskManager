using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Persistence.Repositories;
using TaskManager.Services.Comunication;
using TaskManager.Validators;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
        private static int numberOfStateChanges = 0;
        private static object _lockObj = new object();

        private readonly ITaskRepository taskRepository;
        private readonly ITaskValidator taskValidator;
        public TaskService(ITaskRepository taskRepository, ITaskValidator taskValidator)
        {
            this.taskRepository = taskRepository;
            this.taskValidator = taskValidator;
        }
        public BaseResponse<IEnumerable<TaskDTO>> GetTasks()
        {
            try
            {
                var response = new BaseResponse<IEnumerable<TaskDTO>>(taskRepository.GetAll().Select(x => new TaskDTO(x)));
                lock (_lockObj)
                {
                    response.NumberOfStateChanges = numberOfStateChanges;
                }
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<TaskDTO>>($"An error occured: {ex.Message}");
            }
        }

        public BaseResponse<TaskDTO> SaveTask(TaskDTO task)
        {
            try
            {
                var validationResult = taskValidator.ValidateTask(task);
                if (!validationResult.Success)
                {
                    return new BaseResponse<TaskDTO>(validationResult.ErrorMessage);
                }

                var response = new BaseResponse<TaskDTO>(new TaskDTO(taskRepository.Save(task.Map())));
                SetNumberOfStateChanges(response);
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<TaskDTO>($"An error occured: {ex.Message}");
            }
        }

        private void SetNumberOfStateChanges(BaseResponse<TaskDTO> response)
        {
            Interlocked.Increment(ref numberOfStateChanges);
            lock (_lockObj)
            {
                response.NumberOfStateChanges = numberOfStateChanges;
            }
        }

        public BaseResponse<TaskDTO> UpdateTask(TaskDTO task)
        {
            try
            {
                var validationResult = taskValidator.ValidateTask(task);
                if (!validationResult.Success)
                {
                    return new BaseResponse<TaskDTO>(validationResult.ErrorMessage);
                }

                var response = new BaseResponse<TaskDTO>(new TaskDTO(taskRepository.Update(task.Map())));
                SetNumberOfStateChanges(response);
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<TaskDTO>($"An error occured: {ex.Message}");
            }
        }
    }
}
