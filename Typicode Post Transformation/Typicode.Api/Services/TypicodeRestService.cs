using Typicode.Api.Controllers;

namespace Typicode.Api.Services;

public sealed record TypicodeRestService : RestService, ITypicodeRestService
{
    private const string Uri = "https://jsonplaceholder.typicode.com/";

    public TypicodeRestService(IHttpClientFactory httpClientFactory)
        : base(httpClientFactory, Uri)
    {

    }
}
