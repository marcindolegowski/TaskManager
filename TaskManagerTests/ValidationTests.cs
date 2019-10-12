using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Services;
using TaskManager.Services.Comunication;
using TaskManager.Services.Comunication.TaskValidationService;
using TaskManager.Validators;

namespace TaskManagerTests
{
    [TestFixture]
    public class ValidationTests
    {

        [OneTimeSetUp]
        public void ClassInit()
        {

        }

        [SetUp]
        public void Setup()
        {

        }

        [TestCaseSource("ValidateTaskNameTestCases")]
        [Test]
        public void ValidateTaskName(string taskName, bool expectedResult)
        {
            var mockTaskValidationServiceClient = new Mock<ITaskValidationServiceClient>();
            var taskMangerValidator = new TaskValidator(mockTaskValidationServiceClient.Object);

            ValidationResult validationResult = taskMangerValidator.ValidateTaskName(taskName);

            Assert.AreEqual(expectedResult, validationResult.Success);
        }
        public static IEnumerable<TestCaseData> ValidateTaskNameTestCases
        {
            get
            {
                yield return new TestCaseData(new string('t', 50), true);
                yield return new TestCaseData(new string('t', 51), false);
                yield return new TestCaseData(new string('t', 2), false);
                yield return new TestCaseData(new string('t', 3), true);
            }
        }

        [TestCaseSource("ValidateTaskNameUniquenessTestCases")]
        [Test]
        public void ValidateTaskUniqueness(TaskManager.Model.TaskStatus taskStatus, bool nameIsUnique, bool expectedResult)
        {
            var task = new TaskDTO { Status = taskStatus };
            var mockTaskValidationServiceClient = new Mock<ITaskValidationServiceClient>();
            var validationResponse = new ValidationResponse { Success = nameIsUnique };
            mockTaskValidationServiceClient.Setup(x => x.IsTaskNameUnique(task.Name)).Returns(validationResponse);

            var taskMangerValidator = new TaskValidator(mockTaskValidationServiceClient.Object);

            ValidationResult validationResult = taskMangerValidator.ValidateUniqueness(task);

            Assert.AreEqual(expectedResult, validationResult.Success);
        }

        public static IEnumerable<TestCaseData> ValidateTaskNameUniquenessTestCases
        {
            get
            {
                yield return new TestCaseData(TaskManager.Model.TaskStatus.Complete, true, true);
                yield return new TestCaseData(TaskManager.Model.TaskStatus.Open, true, true);
                yield return new TestCaseData(TaskManager.Model.TaskStatus.Open, false, false);
            }
        }


    }
}