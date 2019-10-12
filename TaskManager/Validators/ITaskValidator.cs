using TaskManager.Services.Comunication;

namespace TaskManager.Validators
{
    public interface ITaskValidator
    {
        ValidationResult ValidateTaskName(string taskName);
        ValidationResult ValidateUniqueness(TaskDTO task);
        ValidationResult ValidateTask(TaskDTO task);
    }
}