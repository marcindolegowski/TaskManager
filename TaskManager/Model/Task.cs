using System;

namespace TaskManager.Model
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TaskStatus Status { get; set; }

        public DateTime TimeStamp { get; set; }
    }

    public enum TaskStatus
    {
        Open = 0,
        Complete = 1
    }
}
