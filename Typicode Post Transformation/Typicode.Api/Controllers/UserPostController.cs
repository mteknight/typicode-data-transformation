using Dawn;

using Microsoft.AspNetCore.Mvc;

using Typicode.Api.Domain;
using Typicode.Api.Domain.Services;

namespace Typicode.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserPostController : ControllerBase
{
    private readonly IUserService userService;

    public UserPostController(IUserService userService)
    {
        this.userService = Guard.Argument(userService, nameof(userService)).NotNull().Value;
    }

    [HttpGet]
    [Produces(typeof(IEnumerable<UserPosts>))]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
    {
        var userPosts = await this.userService.TransformPosts(cancellationToken);

        return this.Ok(userPosts);
    }
}
