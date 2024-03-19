using FitnessHelper.Data;
using Microsoft.AspNetCore.Mvc;

namespace FitnessHelper.Endpoints.Foods;

public class GetFoodById
{
    public static string Template => "/foods/{id:int}";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(AppDbContext context, int id)
    {

        var foods = context.Foods.Where(f =>  f.Id == id);

        if (foods is null || !foods.Any())
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = $"No food found with the id: {id}"
            };

            return Results.Problem(problemDetails);
        }

        var foodsResponse = foods.Select(f =>
        new FoodsResponse
        {
            Id = f.Id,
            Name = f.Name,
            UnitOfMeasurement = f.UnitOfMeasurement,
            Qty = f.Qty,
            QtyProt = f.QtyProt,
            QtyCarb = f.QtyCarb,
            QtyFat = f.QtyFat,
            QtyCal = f.QtyCal,
        });

        return Results.Ok(foodsResponse);
    }
}
