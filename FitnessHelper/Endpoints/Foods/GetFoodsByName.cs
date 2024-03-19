using FitnessHelper.Data;
using FitnessHelper.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FitnessHelper.Endpoints.Foods;

public class GetFoodsByName
{
    public static string Template => "/foods/{name}";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(AppDbContext context, string name)
    {

        string lowerName = name.ToLower();

        List<FoodsClass>? foods = context.Foods
            .Where(f => f.Name.ToLower().Contains(name))
            .ToList();

        if (foods is null || !foods.Any())
        {

            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = $"No food found with the name: {name}"
            };

            return Results.Problem(problemDetails);
        }

        IEnumerable<FoodsResponse> foodsResponse = foods.Select(f =>
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
