using LunchSync.Core.Common.Abstractions;
using LunchSync.Core.Common.Enums;
using LunchSync.Core.Modules.Auth.Entities;

namespace LunchSync.Core.Modules.RestaurantsAndDishes.Entities;

public class Submission : BaseEntity
{
    public Guid UserId { get; set; }
    public string RestaurantName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string? GoogleMapsUrl { get; set; }
    public PriceTier? PriceTier { get; set; }
    public string? Notes { get; set; }
    public SubmissionStatus Status { get; set; } = SubmissionStatus.Pending;
    public Guid? ReviewedBy { get; set; }
    public DateTime? ReviewedAt { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;
    public ICollection<SubmissionPhoto> Photos { get; set; } = new List<SubmissionPhoto>();
    public ICollection<SubmissionDish> SubmissionDishes { get; set; } = new List<SubmissionDish>();
}
