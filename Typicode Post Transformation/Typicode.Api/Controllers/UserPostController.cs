using System.Text.Json;

using Dawn;

using Microsoft.AspNetCore.Mvc;

using Typicode.Api.Domain;

namespace Typicode.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserPostController : ControllerBase
{
    private readonly IHttpClientFactory httpClientFactory;

    public UserPostController(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = Guard.Argument(httpClientFactory, nameof(httpClientFactory)).NotNull().Value;
    }

    [HttpGet]
    [Produces(typeof(IEnumerable<dynamic>))]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
    {
        using var client = this.httpClientFactory.CreateClient();
        client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");

        var users = await GetData<User>(client, "users", cancellationToken);
        var posts = await GetData<Post>(client, "posts", cancellationToken);

        var userPosts = users
            .GroupJoin(posts,
                user => user.Id,
                post => post.UserId,
                (user, posts) => new UserPosts(user, posts.ToArray()));

        return this.Ok(userPosts);
    }

    private static async Task<IEnumerable<TData>?> GetData<TData>(
        HttpClient client,
        string requestUri,
        CancellationToken cancellationToken)
    {
        var response = await client.GetAsync(requestUri, cancellationToken);
        response.EnsureSuccessStatusCode();
        var payload = await response.Content.ReadAsStringAsync(cancellationToken);

        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        return JsonSerializer.Deserialize<IEnumerable<TData>>(payload, options);
    }
}
