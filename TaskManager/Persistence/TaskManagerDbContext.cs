using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Model;

namespace TaskManager.Persistence
{
    public class TaskManagerDbContext : DbContext
    {
        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options) { }

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
