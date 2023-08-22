using CloudCostumers.API.Controllers;
using CloudCostumers.API.Models;
using CloudCostumers.API.Services;
using CloudCostumers.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CloudCostumers.UnitTests.Systems.Controllers;

public class TestUsersController
{
    private Mock<IUsersService> mockUserService()
    {
        return new Mock<IUsersService>();
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnStatusCode200()
    {
        // Arrange
        
        mockUserService()
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());
        
        var sut = new UsersController(mockUserService().Object);

        // Act
        var result = (OkObjectResult)await sut.Get();

        // Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokesUserServiceExcatlyOnce()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());

        var sut = new UsersController(mockUsersService.Object);

        // Act
        var result = (OkObjectResult)await sut.Get();

        //Assert
        mockUsersService.Verify(
            service => service.GetAllUsers(),
            Times.Once
        );
    }
    [Fact]
    public async Task Get_OnFailure_InvokesUserServiceExcatlyOnce()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());

        var sut = new UsersController(mockUsersService.Object);

        // Act
        var result = (NotFoundResult)await sut.Get();

        //Assert
        mockUsersService.Verify(
            service => service.GetAllUsers(),
            Times.Once
        );
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsListOfUsers()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();

        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());

        var sut = new UsersController(mockUsersService.Object);
        
        
        // Act
        var result = await sut.Get();
        
        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)result;
        objectResult.Value.Should().BeOfType<List<User>>();
    }

    [Fact]
    public async Task Get_OnNoUsersFound_Returns404()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();

        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());

        var sut = new UsersController(mockUsersService.Object);
        
        // Act
        var result = await sut.Get();
        
        // Assert
        result.Should().BeOfType<NotFoundResult>();
        var notFoundResult = (NotFoundResult)result;
        notFoundResult.StatusCode.Should().Be(404);
    }

    // Allow us to write parameterized tests

    //[Theory]
    //[InlineData("foo", 1)]
    //[InlineData("bar", 1)]
    //public void Test12(string input, int bar)
    //{

    //}
}