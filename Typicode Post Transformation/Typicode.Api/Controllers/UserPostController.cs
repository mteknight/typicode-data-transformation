using Dawn;

using Microsoft.AspNetCore.Mvc;

using Typicode.Api.Domain;
using Typicode.Api.Services;

namespace Typicode.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserPostController : ControllerBase
{
    private readonly ITypicodeRestService restService;

    public UserPostController(ITypicodeRestService restService)
    {
        this.restService = Guard.Argument(restService, nameof(restService)).NotNull().Value;
    }

    [HttpGet]
    [Produces(typeof(IEnumerable<dynamic>))]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
    {
        var users = await this.restService.GetData<User>("users", cancellationToken);
        var posts = await this.restService.GetData<Post>("posts", cancellationToken);

        var userPosts = users
            .GroupJoin(posts,
                user => user.Id,
                post => post.UserId,
                (user, posts) => new UserPosts(user, (uint)posts.Count()));

        return this.Ok(userPosts);
    }
}
