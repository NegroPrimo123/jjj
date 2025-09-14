using BusinessLogic.Services;
using Domain.Interfaces;
using Domain.Models;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Tests
{
    public class CourseServiceTest
    {
        private readonly CourseService _service;
        private readonly Mock<IRepositoryWrapper> _repositoryWrapperMoq;
        private readonly Mock<ICourseRepository> _courseRepositoryMoq;

        public CourseServiceTest()
        {
            _repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            _courseRepositoryMoq = new Mock<ICourseRepository>();

            _repositoryWrapperMoq.Setup(x => x.Course)
                .Returns(_courseRepositoryMoq.Object);
            _repositoryWrapperMoq.Setup(x => x.Save())
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            _service = new CourseService(_repositoryWrapperMoq.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAll_ShouldReturnAllCourses()
        {
            // arrange
            var courses = new List<Course>
            {
                new Course { CourseId = 1, CourseName = "Math", TeacherId = 1 },
                new Course { CourseId = 2, CourseName = "Physics", TeacherId = 1 }
            };

            _courseRepositoryMoq.Setup(x => x.FindAll())
                .ReturnsAsync(courses);

            // act
            var result = await _service.GetAll();

            // assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            _courseRepositoryMoq.Verify(x => x.FindAll(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetById_ExistingId_ShouldReturnCourse()
        {
            // arrange
            var course = new Course { CourseId = 1, CourseName = "Math", TeacherId = 1 };
            var courses = new List<Course> { course };

            _courseRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<System.Linq.Expressions.Expression<Func<Course, bool>>>()))
                .ReturnsAsync(courses);

            // act
            var result = await _service.GetById(1);

            // assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CourseId);
            Assert.Equal("Math", result.CourseName);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetById_NonExistingId_ShouldThrowException()
        {
            // arrange
            var emptyList = new List<Course>();
            _courseRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<System.Linq.Expressions.Expression<Func<Course, bool>>>()))
                .ReturnsAsync(emptyList);

            // act & assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.GetById(999));
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_ValidCourse_ShouldCreateSuccessfully()
        {
            // arrange
            var validCourse = new Course
            {
                CourseName = "Math",
                TeacherId = 1
            };

            _courseRepositoryMoq.Setup(x => x.Create(It.IsAny<Course>()))
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            // act
            await _service.Create(validCourse);

            // assert
            _courseRepositoryMoq.Verify(x => x.Create(It.IsAny<Course>()), Times.Once);
            _repositoryWrapperMoq.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_NullCourse_ShouldThrowArgumentNullException()
        {
            // act & assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public async System.Threading.Tasks.Task Create_InvalidCourseName_ShouldThrowArgumentException(string courseName)
        {
            // arrange
            var invalidCourse = new Course { CourseName = courseName, TeacherId = 1 };

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.Create(invalidCourse));
        }

        [Fact]
        public async System.Threading.Tasks.Task Update_ValidCourse_ShouldUpdateSuccessfully()
        {
            // arrange
            var course = new Course
            {
                CourseId = 1,
                CourseName = "Updated Math",
                TeacherId = 1
            };

            _courseRepositoryMoq.Setup(x => x.Update(It.IsAny<Course>()))
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            // act
            await _service.Update(course);

            // assert
            _courseRepositoryMoq.Verify(x => x.Update(It.IsAny<Course>()), Times.Once);
            _repositoryWrapperMoq.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Delete_ExistingCourse_ShouldDeleteSuccessfully()
        {
            // arrange
            var course = new Course { CourseId = 1, CourseName = "Math", TeacherId = 1 };
            var courses = new List<Course> { course };

            _courseRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<System.Linq.Expressions.Expression<Func<Course, bool>>>()))
                .ReturnsAsync(courses);
            _courseRepositoryMoq.Setup(x => x.Delete(It.IsAny<Course>()))
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            // act
            await _service.Delete(1);

            // assert
            _courseRepositoryMoq.Verify(x => x.Delete(It.IsAny<Course>()), Times.Once);
            _repositoryWrapperMoq.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Delete_NonExistingCourse_ShouldThrowException()
        {
            // arrange
            var emptyList = new List<Course>();
            _courseRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<System.Linq.Expressions.Expression<Func<Course, bool>>>()))
                .ReturnsAsync(emptyList);

            // act & assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.Delete(999));
        }
    }
}