using System.Collections.Generic;
using System.Threading.Tasks;

using Dawn;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Typicode.Api.Controllers;
using Typicode.Api.Domain;
using Typicode.Api.Domain.Services;

using Xunit;

namespace Typicode.Api.Tests.Integration.Controllers;

public class UserPostControllerTests
{
    private readonly IUserService userService;

    public UserPostControllerTests(IUserService userService)
    {
        this.userService = Guard.Argument(userService, nameof(userService)).NotNull().Value;
    }

    [Fact]
    public async Task GivenDataAvailable_WhenUserPostTransformationRequested_ThenReturnTransformedData()
    {
        // Arrange
        var sut = new UserPostController(this.userService);

        // Act
        var result = await sut.Get();

        // Assert
        result.As<OkObjectResult>().Should().NotBeNull("A response is always expected.");
        result.As<OkObjectResult>().Value.Should().NotBeNull("An array of UserPosts instances are expected.");
        result.As<OkObjectResult>().Value.As<IEnumerable<UserPosts>>().Should().NotBeEmpty("At least one item is expected.");
    }
}
