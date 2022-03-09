using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Dawn;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Typicode.Api.Controllers;
using Typicode.Api.Domain;

using Xunit;

namespace Typicode.Api.Tests.Integration.Controllers;

public class UserPostControllerTests
{
    private readonly IHttpClientFactory httpClientFactory;

    public UserPostControllerTests(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = Guard.Argument(httpClientFactory, nameof(httpClientFactory)).NotNull().Value;
    }

    [Fact]
    public async Task GivenDataAvailable_WhenUserPostTransformationRequested_ThenReturnTransformedData()
    {
        // Arrange
        var sut = new UserPostController(this.httpClientFactory);

        // Act
        var result = await sut.Get();

        // Assert
        result.As<OkObjectResult>().Should().NotBeNull("A response is always expected.");
        result.As<OkObjectResult>().Value.Should().NotBeNull("An array of UserPosts instances are expected.");
        result.As<OkObjectResult>().Value.As<IEnumerable<UserPosts>>().Should().NotBeEmpty("At least one item is expected.");
    }
}
