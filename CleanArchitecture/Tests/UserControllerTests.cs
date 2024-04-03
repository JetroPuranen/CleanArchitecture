using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Controllers;
using CleanArchitecture.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;


namespace CleanArchitecture.Tests
{
    public class UserControllerTests
    {
        [Fact]
        public async Task RegisterUserAsync_ReturnsCreatedResult_WhenRegistrationSucceeds()
        {
            var mockService = new Mock<IUserRegistrationService>();

            mockService.Setup(service => service.RegisterUserAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(true);
            var controller = new UserController(mockService.Object);

            var result = await controller.RegisterUserAsync(new UserRegistrationRequest { Name = "test", Email = "test@email.com" });

            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task RegisterUserAsync_ReturnsBadRequest_WhenRegistrationFails()
        {
            var mockService = new Mock<IUserRegistrationService>();

            mockService.Setup(service => service.RegisterUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            var controller = new UserController(mockService.Object);

            var result = await controller.RegisterUserAsync(new UserRegistrationRequest { Name = "test", Email = "fail@email.com" });

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }

  
}
