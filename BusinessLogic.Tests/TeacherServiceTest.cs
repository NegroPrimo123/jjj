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
    public class TeacherServiceTest
    {
        private readonly TeacherService _service;
        private readonly Mock<IRepositoryWrapper> _repositoryWrapperMoq;
        private readonly Mock<ITeacherRepository> _teacherRepositoryMoq;

        public TeacherServiceTest()
        {
            _repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            _teacherRepositoryMoq = new Mock<ITeacherRepository>();

            _repositoryWrapperMoq.Setup(x => x.Teacher)
                .Returns(_teacherRepositoryMoq.Object);
            _repositoryWrapperMoq.Setup(x => x.Save())
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            _service = new TeacherService(_repositoryWrapperMoq.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_ValidTeacher_ShouldCreateSuccessfully()
        {
            // arrange
            var validTeacher = new Teacher
            {
                FirstName = "John",
                LastName = "Doe"
            };

            _teacherRepositoryMoq.Setup(x => x.Create(It.IsAny<Teacher>()))
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            // act
            await _service.Create(validTeacher);

            // assert
            _teacherRepositoryMoq.Verify(x => x.Create(It.IsAny<Teacher>()), Times.Once);
            _repositoryWrapperMoq.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_NullTeacher_ShouldThrowArgumentNullException()
        {
            // act & assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public async System.Threading.Tasks.Task Create_InvalidFirstName_ShouldThrowArgumentException(string firstName)
        {
            // arrange
            var invalidTeacher = new Teacher
            {
                FirstName = firstName,
                LastName = "Doe"
            };

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.Create(invalidTeacher));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public async System.Threading.Tasks.Task Create_InvalidLastName_ShouldThrowArgumentException(string lastName)
        {
            // arrange
            var invalidTeacher = new Teacher
            {
                FirstName = "John",
                LastName = lastName
            };

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.Create(invalidTeacher));
        }

        [Fact]
        public async System.Threading.Tasks.Task GetById_ExistingId_ShouldReturnTeacher()
        {
            // arrange
            var teacher = new Teacher
            {
                TeacherId = 1,
                FirstName = "John",
                LastName = "Doe"
            };
            var teachers = new List<Teacher> { teacher };

            _teacherRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<System.Linq.Expressions.Expression<Func<Teacher, bool>>>()))
                .ReturnsAsync(teachers);

            // act
            var result = await _service.GetById(1);

            // assert
            Assert.NotNull(result);
            Assert.Equal(1, result.TeacherId);
            Assert.Equal("John", result.FirstName);
        }
    }
}