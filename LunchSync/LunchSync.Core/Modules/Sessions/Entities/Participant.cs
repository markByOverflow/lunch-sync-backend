﻿using System.Text.Json.Serialization;

using LunchSync.Core.Common.Abstractions;
using LunchSync.Core.Modules.Auth.Entities;

namespace LunchSync.Core.Modules.Sessions.Entities;

public class Participant : BaseEntity
{
    public Guid SessionId { get; set; }
    public Guid? UserId { get; set; }  // NULL for anonymous users

    public string Nickname { get; set; } = null!;
    //unique(SessionId, Nickname)

    public List<float>? PrefVector { get; set; }
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    public DateTime? VotedAt { get; set; }

    //navigation
    [JsonIgnore]
    public Session Session { get; set; } = null!;

    [JsonIgnore]
    public User User { get; set; } = null!;
}
