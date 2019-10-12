using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Model;

namespace TaskManager.Persistence
{
    public class DataGenerator
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TaskManagerDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<TaskManagerDbContext>>()))
            {
                if (context.Tasks.Any())
                {
                    return;
                }

                context.Tasks.Add(new Task
                {
                    Id= 1,
                    Name = "Clean kitechn",
                    Status = TaskStatus.Complete,
                    TimeStamp = DateTime.Now.AddMinutes(-5)
                });

                context.Tasks.Add(new Task
                {
                    Id = 2,
                    Name = "Payment for flat rent",
                    Status = TaskStatus.Open,
                    TimeStamp = DateTime.Now.AddMinutes(-10)
                });

                context.SaveChanges();
            }
        }
    }
}
