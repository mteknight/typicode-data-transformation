using System.Text.Json;

using Dawn;

namespace Typicode.Api.Services;

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