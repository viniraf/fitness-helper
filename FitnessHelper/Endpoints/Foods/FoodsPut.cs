using FitnessHelper.Data;
using FitnessHelper.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FitnessHelper.Endpoints.Foods;

public class FoodsPut
{
    public static string Template => "/foods{id:int}";

    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };

    public static Delegate Handle => Action;

    public static async Task<IResult> Action([FromRoute] int id, FoodsRequest foodsRequest, AppDbContext context)
    {
        FoodsClass? food = context.Foods.FirstOrDefault(f => f.Id == id);

        if (food is null)
        {
            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = $"No food found with the id: {id}"
            };

            return Results.Problem(problemDetails);
        }

        double qtyCalPerGram = (foodsRequest.QtyProt * 4) + (foodsRequest.QtyCarb * 4) + (foodsRequest.QtyFat * 7);

        food.Name = foodsRequest.Name;
        food.UnitOfMeasurement = foodsRequest.UnitOfMeasurement;
        food.Qty = foodsRequest.Qty;
        food.QtyProt = foodsRequest.QtyProt;
        food.QtyCarb = foodsRequest.QtyCarb;
        food.QtyFat = foodsRequest.QtyFat;
        food.QtyCal = qtyCalPerGram;

        await context.SaveChangesAsync();

        return Results.Ok("Food has been update!");
    }

}
