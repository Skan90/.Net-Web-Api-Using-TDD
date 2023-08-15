using CloudCostumers.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Xunit;

namespace CloudCostumers.UnitTests.Systems.Controllers;

public class TestUsersController
{
    [Fact]
    public async Task Get_OnSuccess_ReturnStatusCode200()
    {
        // Arrange
        var sut = new UsersController();

        // Act
        var result = (OkObjectResult)await sut.Get();

        // Assert
        result.StatusCode.Should().Be(200);
    }
    [Fact]
    public async Task Get_OnSuccess_InvokesUserService()
    {
        // Arrange
        var mockUserService = Mock<IUserService>();
        var sut = new UsersController(mockUserService.Object);

        // Act
        var result = (OkObjectResult)await sut.Get();

        //Assert
    }

    //[Theory] // Allow us to write parameterized tests
    //[InlineData("foo", 1)]
    //[InlineData("bar", 1)]
    //public void Test12(string input, int bar)
    //{

    //}
}