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
    public class SubmissionServiceTest
    {
        private readonly SubmissionService _service;
        private readonly Mock<IRepositoryWrapper> _repositoryWrapperMoq;
        private readonly Mock<ISubmissionRepository> _submissionRepositoryMoq;

        public SubmissionServiceTest()
        {
            _repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            _submissionRepositoryMoq = new Mock<ISubmissionRepository>();

            _repositoryWrapperMoq.Setup(x => x.Submission)
                .Returns(_submissionRepositoryMoq.Object);
            _repositoryWrapperMoq.Setup(x => x.Save())
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            _service = new SubmissionService(_repositoryWrapperMoq.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_ValidSubmission_ShouldCreateSuccessfully()
        {
            // arrange
            var validSubmission = new Submission
            {
                TeamId = 1,
                TaskId = 1,
                SubmissionDate = DateTime.Now,
                FilePath = "/files/analysis.docx",
                Status = "Submitted"
            };

            _submissionRepositoryMoq.Setup(x => x.Create(It.IsAny<Submission>()))
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            // act
            await _service.Create(validSubmission);

            // assert
            _submissionRepositoryMoq.Verify(x => x.Create(It.IsAny<Submission>()), Times.Once);
            _repositoryWrapperMoq.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_NullSubmission_ShouldThrowArgumentNullException()
        {
            // act & assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public async System.Threading.Tasks.Task Create_InvalidFilePath_ShouldThrowArgumentException(string filePath)
        {
            // arrange
            var invalidSubmission = new Submission
            {
                TeamId = 1,
                TaskId = 1,
                SubmissionDate = DateTime.Now,
                FilePath = filePath,
                Status = "Submitted"
            };

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.Create(invalidSubmission));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public async System.Threading.Tasks.Task Create_InvalidStatus_ShouldThrowArgumentException(string status)
        {
            // arrange
            var invalidSubmission = new Submission
            {
                TeamId = 1,
                TaskId = 1,
                SubmissionDate = DateTime.Now,
                FilePath = "/files/analysis.docx",
                Status = status
            };

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.Create(invalidSubmission));
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_SubmissionWithFutureDate_ShouldThrowArgumentException()
        {
            // arrange
            var invalidSubmission = new Submission
            {
                TeamId = 1,
                TaskId = 1,
                SubmissionDate = DateTime.Now.AddDays(1), // Дата в будущем
                FilePath = "/files/analysis.docx",
                Status = "Submitted"
            };

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.Create(invalidSubmission));
        }
    }
}