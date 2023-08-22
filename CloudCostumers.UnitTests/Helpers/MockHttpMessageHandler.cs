﻿using System.Net;
using System.Net.Http.Headers;
using CloudCostumers.API.Models;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace CloudCostumers.UnitTests.Helpers;

internal static class MockHttpMessageHandler<T> {
    internal static Mock<HttpMessageHandler> SetupBasicGetResourceList(List<T> expectedResponse) {
        var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) {
            Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
        };
        mockResponse.Content.Headers.ContentType =
            new MediaTypeHeaderValue("application/json");

        var handlerMock = new Mock<HttpMessageHandler>();

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);

        return handlerMock;
    }
    
    public static Mock<HttpMessageHandler> SetupBasicGetResourceList(List<T> expectedResponse, HttpRequestMessage expectedRequest)
    {
        var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
        };
        mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var handlerMock = new Mock<HttpMessageHandler>();

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get &&
                    req.RequestUri == expectedRequest.RequestUri), // Compare the request URIs
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);

        return handlerMock;
    }
    public static Mock<HttpMessageHandler> SetupBasicGetResourceList(List<T> expectedResponse, string endpoint) {
        var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) {
            Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
        };
        mockResponse.Content.Headers.ContentType =
            new MediaTypeHeaderValue("application/json");

        var handlerMock = new Mock<HttpMessageHandler>();

        var httpRequestMessage = new HttpRequestMessage {
            RequestUri = new Uri(endpoint),
            Method = HttpMethod.Get
        };

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                httpRequestMessage,
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);

        return handlerMock;
    }

    public static Mock<HttpMessageHandler> SetupReturn404() {var mockResponse = new HttpResponseMessage(HttpStatusCode.NotFound) {
            Content = new StringContent("")
        };
        mockResponse.Content.Headers.ContentType =
            new MediaTypeHeaderValue("application/json");

        var handlerMock = new Mock<HttpMessageHandler>();

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);

        return handlerMock;

    }

}