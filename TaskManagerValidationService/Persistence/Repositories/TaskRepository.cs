using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagerValidationService.Model;

namespace TaskManagerValidationService.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskValidatorDbContext context;

        public TaskRepository(TaskValidatorDbContext context)
        {
            this.context = context;
        }

        public bool TaskExist(string name)
        {
            return context.Tasks.Any(x => x.Name == name);
        }
    }
}
