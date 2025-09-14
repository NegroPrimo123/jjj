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
    public class GradeServiceTest
    {
        private readonly GradeService _service;
        private readonly Mock<IRepositoryWrapper> _repositoryWrapperMoq;
        private readonly Mock<IGradeRepository> _gradeRepositoryMoq;

        public GradeServiceTest()
        {
            _repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            _gradeRepositoryMoq = new Mock<IGradeRepository>();

            _repositoryWrapperMoq.Setup(x => x.Grade)
                .Returns(_gradeRepositoryMoq.Object);
            _repositoryWrapperMoq.Setup(x => x.Save())
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            _service = new GradeService(_repositoryWrapperMoq.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_ValidGrade_ShouldCreateSuccessfully()
        {
            // arrange
            var validGrade = new Grade
            {
                SubmissionId = 1,
                TeacherId = 1,
                Score = 85, 
                Feedback = "Good work",
                GradingDate = DateOnly.FromDateTime(DateTime.Now)
            };

            _gradeRepositoryMoq.Setup(x => x.Create(It.IsAny<Grade>()))
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            // act
            await _service.Create(validGrade);

            // assert
            _gradeRepositoryMoq.Verify(x => x.Create(It.IsAny<Grade>()), Times.Once);
            _repositoryWrapperMoq.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_NullGrade_ShouldThrowArgumentNullException()
        {
            // act & assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(null));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(101)]
        [InlineData(150)]
        public async System.Threading.Tasks.Task Create_InvalidScore_ShouldThrowArgumentException(decimal invalidScore)
        {
            // arrange
            var invalidGrade = new Grade
            {
                SubmissionId = 1,
                TeacherId = 1,
                Score = (int)invalidScore,
                Feedback = "Test",
                GradingDate = DateOnly.FromDateTime(DateTime.Now)
            };

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.Create(invalidGrade));
        }

        [Fact]
        public async System.Threading.Tasks.Task GetById_ExistingId_ShouldReturnGrade()
        {
            // arrange
            var grade = new Grade { GradeId = 1, Score = 90 };
            var grades = new List<Grade> { grade };

            _gradeRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<System.Linq.Expressions.Expression<Func<Grade, bool>>>()))
                .ReturnsAsync(grades);

            // act
            var result = await _service.GetById(1);

            // assert
            Assert.NotNull(result);
            Assert.Equal(1, result.GradeId);
            Assert.Equal(90, result.Score);
        }
    }
}