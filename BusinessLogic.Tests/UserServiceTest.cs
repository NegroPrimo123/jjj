// StudentServiceTest.cs - дополнительные тесты
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
    public class StudentServiceTest
    {
        private readonly StudentService _service;
        private readonly Mock<IRepositoryWrapper> _repositoryWrapperMoq;
        private readonly Mock<IStudentRepository> _studentRepositoryMoq;

        public StudentServiceTest()
        {
            _repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            _studentRepositoryMoq = new Mock<IStudentRepository>();

            _repositoryWrapperMoq.Setup(x => x.Student)
                .Returns(_studentRepositoryMoq.Object);
            _repositoryWrapperMoq.Setup(x => x.Save())
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            _service = new StudentService(_repositoryWrapperMoq.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAll_ShouldReturnAllStudents()
        {
            // arrange
            var students = new List<Student>
            {
                new Student { StudentId = 1, FirstName = "John", LastName = "Doe", GroupId = 1 },
                new Student { StudentId = 2, FirstName = "Jane", LastName = "Smith", GroupId = 1 }
            };

            _studentRepositoryMoq.Setup(x => x.FindAll())
                .ReturnsAsync(students);

            // act
            var result = await _service.GetAll();

            // assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            _studentRepositoryMoq.Verify(x => x.FindAll(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetById_ExistingId_ShouldReturnStudent()
        {
            // arrange
            var student = new Student { StudentId = 1, FirstName = "John", LastName = "Doe", GroupId = 1 };
            var students = new List<Student> { student };

            _studentRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<System.Linq.Expressions.Expression<Func<Student, bool>>>()))
                .ReturnsAsync(students);

            // act
            var result = await _service.GetById(1);

            // assert
            Assert.NotNull(result);
            Assert.Equal(1, result.StudentId);
            Assert.Equal("John", result.FirstName);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetById_NonExistingId_ShouldThrowException()
        {
            // arrange
            var emptyList = new List<Student>();
            _studentRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<System.Linq.Expressions.Expression<Func<Student, bool>>>()))
                .ReturnsAsync(emptyList);

            // act & assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.GetById(999));
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_ValidStudent_ShouldCreateSuccessfully()
        {
            // arrange
            var validStudent = new Student
            {
                FirstName = "John",
                LastName = "Doe",
                GroupId = 1
            };

            _studentRepositoryMoq.Setup(x => x.Create(It.IsAny<Student>()))
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            // act
            await _service.Create(validStudent);

            // assert
            _studentRepositoryMoq.Verify(x => x.Create(It.IsAny<Student>()), Times.Once);
            _repositoryWrapperMoq.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Update_ValidStudent_ShouldUpdateSuccessfully()
        {
            // arrange
            var student = new Student
            {
                StudentId = 1,
                FirstName = "John",
                LastName = "Updated",
                GroupId = 1
            };

            _studentRepositoryMoq.Setup(x => x.Update(It.IsAny<Student>()))
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            // act
            await _service.Update(student);

            // assert
            _studentRepositoryMoq.Verify(x => x.Update(It.IsAny<Student>()), Times.Once);
            _repositoryWrapperMoq.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Delete_ExistingStudent_ShouldDeleteSuccessfully()
        {
            // arrange
            var student = new Student { StudentId = 1, FirstName = "John", LastName = "Doe", GroupId = 1 };
            var students = new List<Student> { student };

            _studentRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<System.Linq.Expressions.Expression<Func<Student, bool>>>()))
                .ReturnsAsync(students);
            _studentRepositoryMoq.Setup(x => x.Delete(It.IsAny<Student>()))
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            // act
            await _service.Delete(1);

            // assert
            _studentRepositoryMoq.Verify(x => x.Delete(It.IsAny<Student>()), Times.Once);
            _repositoryWrapperMoq.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Delete_NonExistingStudent_ShouldThrowException()
        {
            // arrange
            var emptyList = new List<Student>();
            _studentRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<System.Linq.Expressions.Expression<Func<Student, bool>>>()))
                .ReturnsAsync(emptyList);

            // act & assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.Delete(999));
        }
    }
}