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
    public class ProjectServiceTest
    {
        private readonly ProjectService _service;
        private readonly Mock<IRepositoryWrapper> _repositoryWrapperMoq;
        private readonly Mock<IProjectRepository> _projectRepositoryMoq;

        public ProjectServiceTest()
        {
            _repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            _projectRepositoryMoq = new Mock<IProjectRepository>();

            _repositoryWrapperMoq.Setup(x => x.Project)
                .Returns(_projectRepositoryMoq.Object);
            _repositoryWrapperMoq.Setup(x => x.Save())
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            _service = new ProjectService(_repositoryWrapperMoq.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_ValidProject_ShouldCreateSuccessfully()
        {
            // arrange
            var validProject = new Project
            {
                ProjectName = "Web Application",
                Description = "Build a web app",
                CourseId = 1,
                MaxScore = 100,
                Deadline = DateOnly.FromDateTime(DateTime.Now.AddDays(30))
            };

            _projectRepositoryMoq.Setup(x => x.Create(It.IsAny<Project>()))
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            // act
            await _service.Create(validProject);

            // assert
            _projectRepositoryMoq.Verify(x => x.Create(It.IsAny<Project>()), Times.Once);
            _repositoryWrapperMoq.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_NullProject_ShouldThrowArgumentNullException()
        {
            // act & assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public async System.Threading.Tasks.Task Create_InvalidProjectName_ShouldThrowArgumentException(string projectName)
        {
            // arrange
            var invalidProject = new Project
            {
                ProjectName = projectName,
                Description = "Build a web app",
                CourseId = 1,
                MaxScore = 100,
                Deadline = DateOnly.FromDateTime(DateTime.Now.AddDays(30))
            };

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.Create(invalidProject));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async System.Threading.Tasks.Task Create_InvalidMaxScore_ShouldThrowArgumentException(int maxScore)
        {
            // arrange
            var invalidProject = new Project
            {
                ProjectName = "Test Project",
                Description = "Test",
                CourseId = 1,
                MaxScore = maxScore,
                Deadline = DateOnly.FromDateTime(DateTime.Now.AddDays(30))
            };

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.Create(invalidProject));
        }
    }
}