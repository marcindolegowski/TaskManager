using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Services;
using TaskManager.Services.Comunication;
using TaskManager.Services.Comunication.TaskValidationService;
using TaskManager.Validators;

namespace TaskManagerTests
{
    public class TaskValidatorTest
    {

        [TestCase(true, true)]
        [TestCase(false, false)]
        [Test]
        public void ValidateTask(bool isUnique, bool expectedResult)
        {
            var mockTaskValidationServiceClient = new Mock<ITaskValidationServiceClient>();
            mockTaskValidationServiceClient.Setup(x => x.IsTaskNameUnique(It.IsAny<string>()))
                .Returns(new ValidationResponse { Message = It.IsAny<string>(), Success = isUnique });

            var taskDTO = new TaskDTO { Name = "Test1" };

            var taskValidator = new TaskValidator(mockTaskValidationServiceClient.Object);
            ValidationResult validationResult = taskValidator.ValidateTask(taskDTO);

            Assert.AreEqual(expectedResult, validationResult.Success);
        }
    }
}
