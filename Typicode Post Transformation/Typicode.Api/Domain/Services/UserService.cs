using Dawn;

using Typicode.Api.Controllers;
using Typicode.Api.Services;

namespace Typicode.Api.Domain.Services;

public sealed record UserService : IUserService
{
    private readonly ITypicodeRestService restService;

    public UserService(ITypicodeRestService restService)
    {
        this.restService = Guard.Argument(restService, nameof(restService)).NotNull().Value;
    }

    public async Task<IEnumerable<UserPosts>> TransformPosts(CancellationToken cancellationToken)
    {
        var users = await this.restService.GetData<User>("users", cancellationToken);
        var posts = await this.restService.GetData<Post>("posts", cancellationToken);

        return users
            .GroupJoin(posts,
                user => user.Id,
                post => post.UserId,
                (user,
                    posts) => new UserPosts(user, (uint)posts.Count()));
    }
}