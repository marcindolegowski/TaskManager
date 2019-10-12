using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerValidationService.Model;
using TaskManagerValidationService.Persistence;
using TaskManagerValidationService.Persistence.Repositories;
using TaskManagerValidationService.Services.Comunication;

namespace TaskManagerTests
{
    [TestFixture(Category = "Integration")]
    class TaskValidationServiceTests
    {
        private TaskValidatorDbContext dbContext;
        [OneTimeSetUp]
        public void ClassInit()
        {
            var options = new DbContextOptionsBuilder<TaskValidatorDbContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString())
                     .Options;
            dbContext = new TaskValidatorDbContext(options);
            dbContext.Tasks.Add(new Task { Id = 1, Name = "Task 1a" });
            dbContext.Tasks.Add(new Task { Id = 2, Name = "Task 2a" });
            dbContext.Tasks.Add(new Task { Id = 3, Name = "Task 3a" });
            dbContext.SaveChanges();

        }

        [TestCase("Task 1a", false)]
        [TestCase("Task 2a", false)]
        [TestCase("Task 4b", true)]
        [Test]
        public void IsTaskNameUnique_ReturnsCorrectSuccessStatus(string taskName, bool expectedResult)
        {
            var taskRepository = new TaskRepository(dbContext);
            var taskValidationSerivce = new TaskManagerValidationService.Services.TaskValidationService(taskRepository);
            BaseResponse response = taskValidationSerivce.IsTaskNameUnique(taskName);
            Assert.AreEqual(expectedResult, response.Success);
        }

        [Test]
        public void IsTaskNameUnique_ReturnsExceptionMessageAndFalseSuccess()
        {
            string exceptionMessageText = "test123";
            var mockTaskRepository = new Mock<ITaskRepository>(MockBehavior.Strict);
            var r = mockTaskRepository.Setup(m => m.TaskExist(It.IsAny<string>()))
        .Throws(new Exception(exceptionMessageText));
            var taskValidationSerivce = new TaskManagerValidationService.Services.TaskValidationService(mockTaskRepository.Object);
            BaseResponse response = taskValidationSerivce.IsTaskNameUnique(It.IsAny<string>());
            string expectedErrorMessage = $"An error occured: {exceptionMessageText}";
            
            Assert.IsFalse(response.Success);
            Assert.AreEqual(expectedErrorMessage, response.Message);
        }
    }
}
