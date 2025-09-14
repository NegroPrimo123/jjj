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
    public class ProjectTeamServiceTest
    {
        private readonly ProjectTeamService _service;
        private readonly Mock<IRepositoryWrapper> _repositoryWrapperMoq;
        private readonly Mock<IProjectTeamRepository> _projectTeamRepositoryMoq;

        public ProjectTeamServiceTest()
        {
            _repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            _projectTeamRepositoryMoq = new Mock<IProjectTeamRepository>();

            _repositoryWrapperMoq.Setup(x => x.ProjectTeam)
                .Returns(_projectTeamRepositoryMoq.Object);
            _repositoryWrapperMoq.Setup(x => x.Save())
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            _service = new ProjectTeamService(_repositoryWrapperMoq.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_ValidProjectTeam_ShouldCreateSuccessfully()
        {
            // arrange
            var validProjectTeam = new Projectteam
            {
                TeamName = "Team Alpha",
                ProjectId = 1
            };

            _projectTeamRepositoryMoq.Setup(x => x.Create(It.IsAny<Projectteam>()))
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            // act
            await _service.Create(validProjectTeam);

            // assert
            _projectTeamRepositoryMoq.Verify(x => x.Create(It.IsAny<Projectteam>()), Times.Once);
            _repositoryWrapperMoq.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_NullProjectTeam_ShouldThrowArgumentNullException()
        {
            // act & assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public async System.Threading.Tasks.Task Create_InvalidTeamName_ShouldThrowArgumentException(string teamName)
        {
            // arrange
            var invalidProjectTeam = new Projectteam
            {
                TeamName = teamName,
                ProjectId = 1
            };

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.Create(invalidProjectTeam));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async System.Threading.Tasks.Task Create_InvalidProjectId_ShouldThrowArgumentException(int projectId)
        {
            // arrange
            var invalidProjectTeam = new Projectteam
            {
                TeamName = "Team Alpha",
                ProjectId = projectId
            };

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.Create(invalidProjectTeam));
        }
    }
}