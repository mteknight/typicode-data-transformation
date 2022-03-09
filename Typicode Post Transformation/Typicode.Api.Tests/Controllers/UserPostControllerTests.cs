using System.Net.Http;

using Dawn;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Typicode.Api.Controllers;

using Xunit;

namespace Typicode.Api.Tests.Controllers;

public class UserPostControllerTests
{
    private readonly IHttpClientFactory httpClientFactory;

    public UserPostControllerTests(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = Guard.Argument(httpClientFactory, nameof(httpClientFactory)).NotNull().Value;
    }

    [Fact]
    public void GivenDataAvailable_WhenUserPostTransformationRequested_ThenReturnTransformedData()
    {
        // Arrange
        var sut = new UserPostController(this.httpClientFactory);

        // Act
        var result = sut.Get();

        // Assert
        result.As<OkObjectResult>().Should().NotBeNull();
    }
}
