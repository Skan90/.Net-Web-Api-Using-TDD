using CloudCostumers.API.Config;
using CloudCostumers.API.Models;
using CloudCostumers.API.Services;
using CloudCostumers.UnitTests.Fixtures;
using CloudCostumers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace CloudCostumers.UnitTests.Systems.Services;

public class TestUsersService {
    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest() {
        // Arrange
        var expectedResponse = UsersFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>
            .SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://example.com";
        var config = Options.Create(new UsersApiOptions {
            Endpoint = endpoint
        });
        var sut = new UsersService(httpClient, config);
        
        // Act
        await sut.GetAllUsers();
        
        // Assert
        handlerMock
            .Protected()
            .Verify(
                "SendAsync",
                Times.Exactly(1), 
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            );
    }

    [Fact]
    public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
    {
        // Arrange
        var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://example.com";
        var config = Options.Create(new UsersApiOptions {
            Endpoint = endpoint
        });
        var sut = new UsersService(httpClient, config);
        
        // Act
        var result = await sut.GetAllUsers();

        // Assert
        result.Count.Should().Be(0);
    }

    [Fact]
    public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
    {
        // Arrange
        var expectedResponse = UsersFixture.GetTestUsers();
        var endpoint = "https://example.com/users";
        var handlerMock = MockHttpMessageHandler<User>
            .SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var config = Options.Create(new UsersApiOptions {
            Endpoint = endpoint
        });
        
        var sut = new UsersService(httpClient, config);
        
        // Act
        var result = await sut.GetAllUsers();

        // Assert
        result.Count.Should().Be(expectedResponse.Count);
    }
    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
    {
        // Arrange
        var expectedResponse = UsersFixture.GetTestUsers();
        const string endpoint = "https://example.com/users";

        var expectedRequest = new HttpRequestMessage
        {
            RequestUri = new Uri(endpoint),
            Method = HttpMethod.Get
        };

        var handlerMock = MockHttpMessageHandler<User>
            .SetupBasicGetResourceList(expectedResponse, expectedRequest);

        var httpClient = new HttpClient(handlerMock.Object);
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });

        var sut = new UsersService(httpClient, config);

        // Act
        var result = await sut.GetAllUsers();

        // Assert
        handlerMock
            .Protected()
            .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get &&
                    req.RequestUri.ToString() == endpoint),
                ItExpr.IsAny<CancellationToken>()
            );

    }
}