using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerValidationService.Model;

namespace TaskManagerValidationService.Persistence
{
    public class DataGenerator
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TaskValidatorDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<TaskValidatorDbContext>>()))
            {
                if (context.Tasks.Any())
                {
                    return;
                }

                context.Tasks.Add(new Task
                {
                    Id= 1,
                    Name = "Task 1"
                });

                context.Tasks.Add(new Task
                {
                    Id = 2,
                    Name = "Task 2",
                });

                context.SaveChanges();
            }
        }
    }
}
