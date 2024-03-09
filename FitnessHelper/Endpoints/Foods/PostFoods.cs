using FitnessHelper.Data;
using FitnessHelper.Domain;
using FitnessHelper.Endpoints.Foods;

namespace FitnessHelper.Endpoints.NutritionalInformation;

public class PostFoods
{
    public static string Template => "/foods";

    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

    public static async Task<IResult> Action(FoodsRequest foodsRequest, AppDbContext context)
    {
        if (foodsRequest is null)
        {
            return Results.BadRequest("Please check the data and try again");
        }

        var newFood = new FoodsClass();

        var qtyCalPerGram = (foodsRequest.QtyProtPerGram * 4) + (foodsRequest.QtyCarbPerGram * 4) + (foodsRequest.QtyFatPerGram * 7);

        newFood.Name = foodsRequest.Name;
        newFood.QtyProtPerGram = foodsRequest.QtyProtPerGram;
        newFood.QtyCarbPerGram = foodsRequest.QtyCarbPerGram;
        newFood.QtyFatPerGram = foodsRequest.QtyFatPerGram;
        newFood.QtyCalPerGram = qtyCalPerGram;

        await context.Foods.AddAsync(newFood);
        await context.SaveChangesAsync();

        return Results.Created("/notimplemented", $"{newFood.Name} created successfully");

    }
}
