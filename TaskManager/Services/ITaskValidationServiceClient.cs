using System.Threading.Tasks;
using TaskManager.Services.Comunication.TaskValidationService;

namespace TaskManager.Services
{
    public interface ITaskValidationServiceClient
    {
        Task<ValidationResponse> IsTaskNameUniqueAsync(string taskName);
        ValidationResponse IsTaskNameUnique(string taskName);
    }
}