namespace Typicode.Api.Services;

public interface ITypicodeRestService
{
    Task<IEnumerable<TData>?> GetData<TData>(
        string requestUri,
        CancellationToken cancellationToken);
}