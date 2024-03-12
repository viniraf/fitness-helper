using FitnessHelper.Data;
using FitnessHelper.Endpoints.Foods;

namespace FitnessHelper.Endpoints.NutritionalInformation;

public class GetAllFoods
{
    public static string Template => "/foods";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(AppDbContext context)
    {
        var foods = context.Foods.ToList();

        if (foods is null || !foods.Any())
        {
            return Results.NotFound("No food registered");
        }

        var foodsResponse = foods.Select(f =>
        new FoodsResponse
        {
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
