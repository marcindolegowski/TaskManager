using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagerValidationService.Model;

namespace TaskManagerValidationService.Persistence.Repositories
{
    public interface ITaskRepository
    {
        bool TaskExist(string name);
    }
}
