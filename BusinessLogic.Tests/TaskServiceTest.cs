//using BusinessLogic.Services;
//using Domain.Interfaces;
//using Domain.Models;
//using Moq;
//using Xunit;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace BusinessLogic.Tests
//{
//    public class TaskServiceTest
//    {
//        private readonly TaskService _service;
//        private readonly Mock<IRepositoryWrapper> _repositoryWrapperMoq;
//        private readonly Mock<ITaskRepository> _taskRepositoryMoq;

//        public TaskServiceTest()
//        {
//            _repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
//            _taskRepositoryMoq = new Mock<ITaskRepository>();

//            _repositoryWrapperMoq.Setup(x => x.Task)
//                .Returns(_taskRepositoryMoq.Object);
//            _repositoryWrapperMoq.Setup(x => x.Save())
//                .Returns(System.Threading.Tasks.Task.CompletedTask);

//            _service = new TaskService(_repositoryWrapperMoq.Object);
//        }

//        [Fact]
//        public async System.Threading.Tasks.Task Create_ValidTask_ShouldCreateSuccessfully()
//        {
//            // arrange
//            var validTask = new Task
//            {
//                TaskName = "Implement Login Feature",
//                ProjectId = 1,
//                TaskDeadline = DateOnly.FromDateTime(DateTime.Now.AddDays(30)),
//                TaskMaxScore = 100
//            };

//            _taskRepositoryMoq.Setup(x => x.Create(It.IsAny<System.Threading.Tasks.Task>()))
//                .Returns(System.Threading.Tasks.Task.CompletedTask);

//            // act
//            await _service.Create(validTask);

//            // assert
//            _taskRepositoryMoq.Verify(x => x.Create(It.IsAny<System.Threading.Tasks.Task>()), Times.Once);
//            _repositoryWrapperMoq.Verify(x => x.Save(), Times.Once);
//        }

//        [Fact]
//        public async System.Threading.Tasks.Task Create_NullTask_ShouldThrowArgumentNullException()
//        {
//            // act & assert
//            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(null));
//        }

//        [Theory]
//        [InlineData("")]
//        [InlineData("   ")]
//        [InlineData(null)]
//        public async System.Threading.Tasks.Task Create_InvalidTaskName_ShouldThrowArgumentException(string taskName)
//        {
//            // arrange
//            var invalidTask = new Task
//            {
//                TaskName = taskName,
//                ProjectId = 1,
//                TaskDeadline = DateOnly.FromDateTime(DateTime.Now.AddDays(30)),
//                TaskMaxScore = 100
//            };

//            // act & assert
//            await Assert.ThrowsAsync<ArgumentException>(() => _service.Create(invalidTask));
//        }

//        [Theory]
//        [InlineData(0)]
//        [InlineData(-1)]
//        public async System.Threading.Tasks.Task Create_InvalidProjectId_ShouldThrowArgumentException(int projectId)
//        {
//            // arrange
//            var invalidTask = new Task
//            {
//                TaskName = "Implement Login Feature",
//                ProjectId = 1,
//                TaskDeadline = DateOnly.FromDateTime(DateTime.Now.AddDays(30)),
//                TaskMaxScore = 100
//            };

//            // act & assert
//            await Assert.ThrowsAsync<ArgumentException>(() => _service.Create(invalidTask));
//        }

//        [Fact]
//        public async System.Threading.Tasks.Task Update_ValidTask_ShouldUpdateSuccessfully()
//        {
//            // arrange
//            var validTask = new Task
//            {
//                TaskName = "Implement Login Feature",
//                ProjectId = 1,
//                TaskDeadline = DateOnly.FromDateTime(DateTime.Now.AddDays(30)),
//                TaskMaxScore = 100
//            };

//            _taskRepositoryMoq.Setup(x => x.Update(It.IsAny<System.Threading.Tasks.Task>()))
//                .Returns(System.Threading.Tasks.Task.CompletedTask);

//            // act
//            await _service.Update(validTask);

//            // assert
//            _taskRepositoryMoq.Verify(x => x.Update(It.IsAny<System.Threading.Tasks.Task>()), Times.Once);
//            _repositoryWrapperMoq.Verify(x => x.Save(), Times.Once);
//        }

//        [Fact]
//        public async System.Threading.Tasks.Task Update_NullTask_ShouldThrowArgumentNullException()
//        {
//            // act & assert
//            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Update(null));
//        }

//        [Fact]
//        public async System.Threading.Tasks.Task Delete_ExistingTask_ShouldDeleteSuccessfully()
//        {
//            // arrange
//            var existingTask = new Task { TaskId = 1 };
//            var tasks = new List<System.Threading.Tasks.Task> { existingTask }.AsQueryable();

//            _taskRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<System.Linq.Expressions.Expression<Func<System.Threading.Tasks.Task, bool>>>()))
//                .Returns(tasks);
//            _taskRepositoryMoq.Setup(x => x.Delete(It.IsAny<System.Threading.Tasks.Task>()))
//                .Returns(System.Threading.Tasks.Task.CompletedTask);

//            // act
//            await _service.Delete(1);

//            // assert
//            _taskRepositoryMoq.Verify(x => x.Delete(It.IsAny<System.Threading.Tasks.Task>()), Times.Once);
//            _repositoryWrapperMoq.Verify(x => x.Save(), Times.Once);
//        }

//        [Fact]
//        public async System.Threading.Tasks.Task GetById_ExistingTask_ShouldReturnTask()
//        {
//            // arrange
//            var expectedTask = new Task { TaskId = 1, TaskName = "Test Task" };
//            var tasks = new List<System.Threading.Tasks.Task> { expectedTask }.AsQueryable();

//            _taskRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<System.Linq.Expressions.Expression<Func<System.Threading.Tasks.Task, bool>>>()))
//                .Returns(tasks);

//            // act
//            var result = await _service.GetById(1);

//            // assert
//            Assert.NotNull(result);
//            Assert.Equal(expectedTask.TaskId, result.TaskId);
//            Assert.Equal(expectedTask.TaskName, result.TaskName);
//        }

//        [Fact]
//        public async System.Threading.Tasks.Task GetAll_ShouldReturnAllTasks()
//        {
//            // arrange
//            var tasks = new List<System.Threading.Tasks.Task>
//            {
//                new Task { TaskId = 1, TaskName = "Task 1" },
//                new Task { TaskId = 2, TaskName = "Task 2" }
//            };

//            _taskRepositoryMoq.Setup(x => x.FindAll())
//                .ReturnsAsync(tasks);

//            // act
//            var result = await _service.GetAll();

//            // assert
//            Assert.NotNull(result);
//            Assert.Equal(2, result.Count);
//        }
//    }
//}