namespace LunchSync.Core.Modules.RestaurantsAndDishes.Entities;

public class SubmissionDish
{
    public Guid SubmissionId { get; set; }
    public Guid DishId { get; set; } 

    //Navigation
    public Submission Submission { get; set; } = null!;
    public Dish Dish { get; set; } = null!;
}