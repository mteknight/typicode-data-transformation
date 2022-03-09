﻿using System.Text.Json.Serialization;

namespace Typicode.Api.Domain;

public record User
{
    [JsonConstructor]
    public User(UserId id)
    {
        this.Id = id;
    }

    public UserId Id { get; }
}
