using LunchSync.Core.Common.Abstractions;
using LunchSync.Core.Common.Enums;
using LunchSync.Core.Modules.Auth.Entities;
using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;

namespace LunchSync.Core.Modules.Sessions.Entities;

public class Session : BaseEntity
{
    public string Pin { get; set; } = null!;
    public Guid HostId { get; set; }
    public Guid CollectionId { get; set; }
    public PriceTier PriceTier { get; set; }
    public SessionStatus Status { get; set; } = SessionStatus.Waiting;

    //group-vector
    public List<float>? GroupVector { get; set; }

    //Top-dish/restaurant
    public List<Guid>? TopDishIds { get; set; }
    public List<Guid>? TopRestaurantIds { get; set; }
    public List<Guid>? BoomEliminatedIds { get; set; }

    public Guid? FinalRestaurantId { get; set; }
    public DateTime? VotingStartedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }

    //Navigation
    public User Host { get; set; } = null!;
    public Collection Collection { get; set; } = null!;
    public Restaurant Restaurant { get; set; } = null!;
    public ICollection<Participant> Participants { get; set; } = new List<Participant>();
}
