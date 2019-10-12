using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagerValidationService.Model;

namespace TaskManagerValidationService.Persistence
{
    public class TaskValidatorDbContext : DbContext
    {
        public TaskValidatorDbContext(DbContextOptions<TaskValidatorDbContext> options) : base(options) { }

        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Task>().ToTable("Tasks");
            builder.Entity<Task>().HasKey(p => p.Id);
            builder.Entity<Task>().Property(p => p.Id).IsRequired();
            builder.Entity<Task>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<Task>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        }
    }
}
