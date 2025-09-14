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
    public class GroupServiceTest
    {
        private readonly GroupService _service;
        private readonly Mock<IRepositoryWrapper> _repositoryWrapperMoq;
        private readonly Mock<IGroupRepository> _groupRepositoryMoq;

        public GroupServiceTest()
        {
            _repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            _groupRepositoryMoq = new Mock<IGroupRepository>();

            _repositoryWrapperMoq.Setup(x => x.Group)
                .Returns(_groupRepositoryMoq.Object);
            _repositoryWrapperMoq.Setup(x => x.Save())
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            _service = new GroupService(_repositoryWrapperMoq.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_ValidGroup_ShouldCreateSuccessfully()
        {
            // arrange
            var validGroup = new Group
            {
                GroupName = "CS-101"
            };

            _groupRepositoryMoq.Setup(x => x.Create(It.IsAny<Group>()))
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            // act
            await _service.Create(validGroup);

            // assert
            _groupRepositoryMoq.Verify(x => x.Create(It.IsAny<Group>()), Times.Once);
            _repositoryWrapperMoq.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_NullGroup_ShouldThrowArgumentNullException()
        {
            // act & assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public async System.Threading.Tasks.Task Create_InvalidGroupName_ShouldThrowArgumentException(string groupName)
        {
            // arrange
            var invalidGroup = new Group
            {
                GroupName = groupName
            };

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.Create(invalidGroup));
        }
    }
}