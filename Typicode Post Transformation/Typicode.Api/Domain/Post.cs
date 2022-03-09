using System.Text.Json.Serialization;

namespace Typicode.Api.Domain;

public record Post
{
    [JsonConstructor]
    public Post(
        PostId id,
        UserId userId)
    {
        this.Id = id;
        this.UserId = userId;
    }

    public PostId Id { get; }

    public UserId UserId { get; }
}
