using LunchSync.Core.Common.Abstractions;

namespace LunchSync.Core.Modules.RestaurantsAndDishes.Entities;

public class SubmissionPhoto : BaseEntity
{
    public Guid SubmissionId { get; set; } 
    public string PhotoUrl { get; set; } = null!;
    public int DisplayOrder { get; set; } = 0;

    //Navigation
    public Submission Submission { get; set; } = null!;
}