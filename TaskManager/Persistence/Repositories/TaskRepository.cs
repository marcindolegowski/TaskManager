using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Model;

namespace TaskManager.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagerDbContext context;

        public TaskRepository(TaskManagerDbContext context)
        {
            this.context = context;
        }

        public Task Find(int id, DateTime timestamp)
        {
            return context.Tasks.Where(x => x.Id == id && x.TimeStamp == timestamp).SingleOrDefault();
        }

        public IEnumerable<Task> GetAll()
        {
            return context.Tasks.AsQueryable();
        }
        public Task Save(Task task)
        {
            task.TimeStamp = DateTime.Now;
            Task newTask = context.Add(task).Entity;
            context.SaveChanges();
            return newTask;
        }

        public Task Update(Task task)
        {
            Task taskToUpdate = context.Tasks
                .Where(x => x.Id == task.Id && x.TimeStamp == task.TimeStamp)
                .SingleOrDefault();

            if(taskToUpdate == null)
            {
                throw new Exception("Task was updated by another user");
            }

            taskToUpdate.Name = task.Name;
            taskToUpdate.Status = task.Status;
            taskToUpdate.TimeStamp = DateTime.Now;

            Task modifiedTask = context.Update(taskToUpdate).Entity;
            context.SaveChanges();
            return modifiedTask;
        }
    }
}
