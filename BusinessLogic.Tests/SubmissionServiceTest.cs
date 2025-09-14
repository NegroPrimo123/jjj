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
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]
        public async System.Threading.Tasks.Task Create_InvalidIds_ShouldThrowArgumentException(int teamId, int taskId)
        {
            var invalidSubmission = new Submission
            {
                TeamId = teamId,
                TaskId = taskId,
                SubmissionDate = DateTime.Now,
                FilePath = "/files/analysis.docx",
                Status = "Submitted"
            };

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.Create(invalidSubmission));
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

        [Fact]
        public async System.Threading.Tasks.Task Create_SubmissionDateInFuture_ShouldThrowArgumentException()
        {
            // arrange
            var invalidSubmission = new Submission
            {
                TeamId = 1,
                TaskId = 1,
                SubmissionDate = DateTime.Now.AddDays(1), 
                FilePath = "/files/analysis.docx",
                Status = "Submitted"
            };

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.Create(invalidSubmission));
        }

        [Fact]
        public async System.Threading.Tasks.Task Update_ValidSubmission_ShouldUpdateSuccessfully()
        {
            // arrange
            var submission = new Submission
            {
                SubmissionId = 1,
                TeamId = 1,
                TaskId = 1,
                SubmissionDate = DateTime.Now.AddHours(-1),
                FilePath = "/files/updated.docx",
                Status = "Reviewed"
            };

            _submissionRepositoryMoq.Setup(x => x.Update(It.IsAny<Submission>()))
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            // act
            await _service.Update(submission);

            // assert
            _submissionRepositoryMoq.Verify(x => x.Update(It.IsAny<Submission>()), Times.Once);
            _repositoryWrapperMoq.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Update_NullSubmission_ShouldThrowArgumentNullException()
        {
            // act & assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Update(null));
        }

        [Fact]
        public async System.Threading.Tasks.Task Delete_ExistingSubmission_ShouldDeleteSuccessfully()
        {
            // arrange
            var submission = new Submission { SubmissionId = 1 };
            var submissions = new List<Submission> { submission };

            _submissionRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<System.Linq.Expressions.Expression<Func<Submission, bool>>>()))
                .ReturnsAsync(submissions);
            _submissionRepositoryMoq.Setup(x => x.Delete(It.IsAny<Submission>()))
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            // act
            await _service.Delete(1);

            // assert
            _submissionRepositoryMoq.Verify(x => x.Delete(It.IsAny<Submission>()), Times.Once);
            _repositoryWrapperMoq.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Delete_NonExistingSubmission_ShouldThrowException()
        {
            // arrange
            var emptyList = new List<Submission>();
            _submissionRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<System.Linq.Expressions.Expression<Func<Submission, bool>>>()))
                .ReturnsAsync(emptyList);

            // act & assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.Delete(999));
        }

        [Fact]
        public async System.Threading.Tasks.Task GetById_ExistingSubmission_ShouldReturnSubmission()
        {
            // arrange
            var expectedSubmission = new Submission { SubmissionId = 1, FilePath = "/files/test.docx" };
            var submissions = new List<Submission> { expectedSubmission };

            _submissionRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<System.Linq.Expressions.Expression<Func<Submission, bool>>>()))
                .ReturnsAsync(submissions);

            // act
            var result = await _service.GetById(1);

            // assert
            Assert.NotNull(result);
            Assert.Equal(expectedSubmission.SubmissionId, result.SubmissionId);
            Assert.Equal(expectedSubmission.FilePath, result.FilePath);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAll_ShouldReturnAllSubmissions()
        {
            // arrange
            var submissions = new List<Submission>
            {
                new Submission { SubmissionId = 1, FilePath = "/files/test1.docx" },
                new Submission { SubmissionId = 2, FilePath = "/files/test2.docx" }
            };

            _submissionRepositoryMoq.Setup(x => x.FindAll())
                .ReturnsAsync(submissions);

            // act
            var result = await _service.GetAll();

            // assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}