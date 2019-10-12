using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Model;

namespace TaskManager.Services.Comunication
{
    public class TaskDTO
    {
        public TaskDTO() { }
        public TaskDTO(Task task)
        {
            Id = task.Id;
            Name = task.Name;
            Status = task.Status;
            TimeStamp = task.TimeStamp;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime TimeStamp { get; set; }
        public Task Map()
        {
            return new Task
            {
                Id = Id,
                Name = Name,
                Status = Status,
                TimeStamp = TimeStamp
            };
        }
    }
}
