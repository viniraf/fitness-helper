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

        var qtyCalPerGram = (foodsRequest.QtyProt * 4) + (foodsRequest.QtyCarb * 4) + (foodsRequest.QtyFat * 7);

        newFood.Name = foodsRequest.Name;
        newFood.UnitOfMeasurement = foodsRequest.UnitOfMeasurement;
        newFood.Qty = foodsRequest.Qty;
        newFood.QtyProt = foodsRequest.QtyProt;
        newFood.QtyCarb = foodsRequest.QtyCarb;
        newFood.QtyFat = foodsRequest.QtyFat;
        newFood.QtyCal = qtyCalPerGram;

        await context.Foods.AddAsync(newFood);
        await context.SaveChangesAsync();

        return Results.Created("/notimplemented", $"{newFood.Name} created successfully");

    }
}
