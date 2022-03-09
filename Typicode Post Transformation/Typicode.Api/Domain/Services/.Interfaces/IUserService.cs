namespace Typicode.Api.Domain.Services;

public interface IUserService
{
    Task<IEnumerable<UserPosts>> TransformPosts(CancellationToken cancellationToken);
}
