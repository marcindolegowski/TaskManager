using System.Collections.Generic;
using TaskManagerValidationService.Model;
using TaskManagerValidationService.Services.Comunication;

namespace TaskManagerValidationService.Services
{
    public interface ITaskValidationService
    {
        BaseResponse IsTaskNameUnique(string taskName);
    }
}