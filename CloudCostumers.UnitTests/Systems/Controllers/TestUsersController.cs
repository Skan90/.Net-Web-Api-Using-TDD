using CloudCostumers.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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

    //[Theory] // Allow us to write parameterized tests
    //[InlineData("foo", 1)]
    //[InlineData("bar", 1)]
    //public void Test12(string input, int bar)
    //{

    //}
}