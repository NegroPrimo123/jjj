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
    public class StudentTeamServiceTest
    {
        private readonly StudentTeamService _service;
        private readonly Mock<IRepositoryWrapper> _repositoryWrapperMoq;
        private readonly Mock<IStudentTeamRepository> _studentTeamRepositoryMoq;

        public StudentTeamServiceTest()
        {
            _repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            _studentTeamRepositoryMoq = new Mock<IStudentTeamRepository>();

            _repositoryWrapperMoq.Setup(x => x.StudentTeam)
                .Returns(_studentTeamRepositoryMoq.Object);
            _repositoryWrapperMoq.Setup(x => x.Save())
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            _service = new StudentTeamService(_repositoryWrapperMoq.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_ValidStudentTeam_ShouldCreateSuccessfully()
        {
            // arrange
            var validStudentTeam = new Studentteam
            {
                StudentId = 1,
                TeamId = 1
            };

            _studentTeamRepositoryMoq.Setup(x => x.Create(It.IsAny<Studentteam>()))
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            // act
            await _service.Create(validStudentTeam);

            // assert
            _studentTeamRepositoryMoq.Verify(x => x.Create(It.IsAny<Studentteam>()), Times.Once);
            _repositoryWrapperMoq.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_NullStudentTeam_ShouldThrowArgumentNullException()
        {
            // act & assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(null));
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]
        public async System.Threading.Tasks.Task Create_InvalidIds_ShouldThrowArgumentException(int studentId, int teamId)
        {
            // arrange
            var invalidStudentTeam = new Studentteam
            {
                StudentId = studentId,
                TeamId = teamId
            };

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.Create(invalidStudentTeam));
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_DuplicateStudentTeam_ShouldThrowArgumentException()
        {
            // arrange
            var existingStudentTeam = new Studentteam
            {
                StudentId = 1,
                TeamId = 1
            };
            var existingTeams = new List<Studentteam> { existingStudentTeam };

            _studentTeamRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<System.Linq.Expressions.Expression<Func<Studentteam, bool>>>()))
                .ReturnsAsync(existingTeams);

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.Create(existingStudentTeam));
        }
    }
}