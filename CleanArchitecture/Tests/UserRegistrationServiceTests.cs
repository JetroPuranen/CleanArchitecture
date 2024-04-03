using Xunit;
using Moq;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Entities;
namespace CleanArchitecture.Tests
{
    public class UserRegistrationServiceTests
    {

        [Fact]
        public async Task RegisterUserAsync_ReturnsFalse_IfEmailExists()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.EmailExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            var service = new UserRegistrationService(mockRepo.Object);

            var result = await service.RegisterUserAsync("Test user", "test@example.com");
            Assert.False(result);

        }
        [Fact]
        public async Task RegisterUserAsync_ReturnsTrue_IfRegistrationSucceeds()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.EmailExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
            mockRepo.Setup(repo => repo.Addsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            var service = new UserRegistrationService(mockRepo.Object);

            var result = await service.RegisterUserAsync("New user", "new@example.com");

            Assert.True(result);
        }
    }
}
