using Dawn;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Typicode.Api.Controllers;
using Typicode.Api.Services;

using Xunit;

namespace Typicode.Api.Tests.Controllers;

public class UserPostControllerTests
{
    private readonly ITypicodeRestService restService;

    public UserPostControllerTests(ITypicodeRestService restService)
    {
        this.restService = Guard.Argument(restService, nameof(restService)).NotNull().Value;
    }

    [Fact]
    public void GivenDataAvailable_WhenUserPostTransformationRequested_ThenReturnTransformedData()
    {
        // Arrange
        var sut = new UserPostController(this.restService);

        // Act
        var result = sut.Get();

        // Assert
        result.As<OkObjectResult>().Should().NotBeNull();
    }
}
