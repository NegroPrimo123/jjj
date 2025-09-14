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
    public class DepartmentServiceTest
    {
        private readonly DepartmentService _service;
        private readonly Mock<IRepositoryWrapper> _repositoryWrapperMoq;
        private readonly Mock<IDepartmentRepository> _departmentRepositoryMoq;

        public DepartmentServiceTest()
        {
            _repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            _departmentRepositoryMoq = new Mock<IDepartmentRepository>();

            _repositoryWrapperMoq.Setup(x => x.Department)
                .Returns(_departmentRepositoryMoq.Object);
            _repositoryWrapperMoq.Setup(x => x.Save())
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            _service = new DepartmentService(_repositoryWrapperMoq.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_ValidDepartment_ShouldCreateSuccessfully()
        {
            // arrange
            var validDepartment = new Department
            {
                DepartmentName = "Computer Science"
            };

            _departmentRepositoryMoq.Setup(x => x.Create(It.IsAny<Department>()))
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            // act
            await _service.Create(validDepartment);

            // assert
            _departmentRepositoryMoq.Verify(x => x.Create(It.IsAny<Department>()), Times.Once);
            _repositoryWrapperMoq.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_NullDepartment_ShouldThrowArgumentNullException()
        {
            // act & assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public async System.Threading.Tasks.Task Create_InvalidDepartmentName_ShouldThrowArgumentException(string departmentName)
        {
            // arrange
            var invalidDepartment = new Department { DepartmentName = departmentName };

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.Create(invalidDepartment));
        }

        [Fact]
        public async System.Threading.Tasks.Task GetById_ExistingId_ShouldReturnDepartment()
        {
            // arrange
            var department = new Department { DepartmentId = 1, DepartmentName = "Computer Science" };
            var departments = new List<Department> { department };

            _departmentRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<System.Linq.Expressions.Expression<Func<Department, bool>>>()))
                .ReturnsAsync(departments);

            // act
            var result = await _service.GetById(1);

            // assert
            Assert.NotNull(result);
            Assert.Equal(1, result.DepartmentId);
            Assert.Equal("Computer Science", result.DepartmentName);
        }
    }
}