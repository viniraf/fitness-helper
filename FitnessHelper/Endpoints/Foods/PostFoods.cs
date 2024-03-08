using FitnessHelper.Data;
using FitnessHelper.Domain;

namespace FitnessHelper.Endpoints.NutritionalInformation;

public class PostFoods
{
    public static string Template => "/foods";

    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

    public static async Task<IResult> Action(Foods food, AppDbContext context)
    {
        if (food is null)
        {
            return Results.BadRequest("Object null, please check the data and try again");
        }

        var newFood = new Foods();

        var qtyCalPerGram = (food.QtyProtPerGram * 4) + (food.QtyCarbPerGram * 4) + (food.QtyFatPerGram * 7);

        newFood.Name = food.Name;
        newFood.QtyProtPerGram = food.QtyProtPerGram;
        newFood.QtyCarbPerGram = food.QtyCarbPerGram;
        newFood.QtyFatPerGram = food.QtyFatPerGram;
        newFood.QtyCalPerGram = qtyCalPerGram;

        await context.Foods.AddAsync(newFood);
        await context.SaveChangesAsync();

        return Results.Created("/notimplemented", $"{newFood.Name} created!");

    }
}
