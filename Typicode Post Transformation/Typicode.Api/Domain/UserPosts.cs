using System.Text.Json.Serialization;

using Dawn;

namespace Typicode.Api.Domain;

public record UserPosts
{
    [JsonConstructor]
    public UserPosts(
        User user,
        uint totalPosts)
    {
        this.User = Guard.Argument(user, nameof(user)).NotNull().Value;
        this.TotalPosts = totalPosts;
    }

    public User User { get; }

    public uint TotalPosts { get; }
}
