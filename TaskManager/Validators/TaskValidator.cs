using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Services;
using TaskManager.Services.Comunication;

namespace TaskManager.Validators
{
    public class TaskValidator : ITaskValidator
    {
        private readonly ITaskValidationServiceClient taskValidationServiceClient;
        public TaskValidator(ITaskValidationServiceClient taskValidationServiceClient)
        {
            this.taskValidationServiceClient = taskValidationServiceClient;
        }

        public ValidationResult ValidateTask(TaskDTO task)
        {
            var validationNameResult = ValidateTaskName(task.Name);
            if (!validationNameResult.Success)
            {
                return validationNameResult;
            }
            var validationUniqnesResult = ValidateUniqueness(task);
            if (!validationUniqnesResult.Success)
            {
                return validationUniqnesResult;
            }
            return new ValidationResult();
        }

        public ValidationResult ValidateTaskName(string taskName)
        {
            if (taskName.Length < 3 || taskName.Length > 50)
            {
                return new ValidationResult("Task name length must be beetwean 3 and 50 characters");
            }
            return new ValidationResult();
        }

        public ValidationResult ValidateUniqueness(TaskDTO task)
        {
            if (task.Status == Model.TaskStatus.Open)
            {
                var validationUniquenessResponse = taskValidationServiceClient.IsTaskNameUnique(task.Name);
                if (!validationUniquenessResponse.Success)
                {
                    return new ValidationResult("Task name already exists");
                }
            }
            return new ValidationResult();
        }
    }
}
