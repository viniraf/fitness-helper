using FitnessHelper.Data;
using FitnessHelper.Domain;
using FitnessHelper.Endpoints.Foods;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace FitnessHelper.Endpoints.NutritionalInformation;

public class GetAllFoods
{
    public static string Template => "/foods";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(AppDbContext context)
    {
        List<FoodsClass>? foods = context.Foods.ToList();

        if (foods is null || !foods.Any())
        {
            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = $"No food registered"
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
