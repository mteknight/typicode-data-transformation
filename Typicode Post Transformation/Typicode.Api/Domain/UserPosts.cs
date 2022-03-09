using System.Text.Json.Serialization;

using Dawn;

namespace Typicode.Api.Domain;

public record UserPosts
{
    [JsonConstructor]
    public UserPosts(
        User user,
        ICollection<Post> posts)
    {
        this.User = Guard.Argument(user, nameof(user)).NotNull().Value;
        this.Posts = Guard.Argument(posts, nameof(posts)).NotNull().Value;
    }

    public User User { get; }

    public ICollection<Post> Posts { get; }
}
