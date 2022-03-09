using System.Text.Json;

using Dawn;

using Microsoft.AspNetCore.Mvc;

using Typicode.Api.Domain;

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

public abstract record RestService
{
    private readonly IHttpClientFactory httpClientFactory;

    protected RestService(
        IHttpClientFactory httpClientFactory,
        string baseUri)
    {
        this.httpClientFactory = Guard.Argument(httpClientFactory, nameof(httpClientFactory)).NotNull().Value;
        this.BaseUri = Guard.Argument(baseUri, nameof(baseUri)).NotNull().Value;
    }

    private string BaseUri { get; }

    public virtual async Task<IEnumerable<TData>?> GetData<TData>(
        string requestUri,
        CancellationToken cancellationToken)
    {
        using var client = this.CreateConfiguredClient();
        var payload = await GetPayload(client, requestUri, cancellationToken);

        return Deserialize<IEnumerable<TData>>(payload);
    }

    private HttpClient CreateConfiguredClient()
    {
        var client = this.httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(this.BaseUri);

        return client;
    }

    private static async Task<string> GetPayload(
        HttpClient client,
        string requestUri,
        CancellationToken cancellationToken)
    {
        var response = await client.GetAsync(requestUri, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    private static TData? Deserialize<TData>(string payload)
    {
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        return JsonSerializer.Deserialize<TData>(payload, options);
    }
}

public interface ITypicodeRestService
{
    Task<IEnumerable<TData>?> GetData<TData>(
        string requestUri,
        CancellationToken cancellationToken);
}

public sealed record TypicodeRestService : RestService, ITypicodeRestService
{
    private const string Uri = "https://jsonplaceholder.typicode.com/";

    public TypicodeRestService(IHttpClientFactory httpClientFactory)
        : base(httpClientFactory, Uri)
    {

    }
}
