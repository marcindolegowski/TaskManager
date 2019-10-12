using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Model;

namespace TaskManager.Persistence.Repositories
{
    public interface ITaskRepository
    {
        Task Find(int id, DateTime timestamp);
        IEnumerable<Task> GetAll();
        Task Save(Task task);
        Task Update(Task task);
    }
}
