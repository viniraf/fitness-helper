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

        if (foods is null)
        {
            return Results.NotFound("No food registered");
        }

        var foodsResponse = foods.Select(f =>
        new FoodsResponse
        {
            Name = f.Name,
            QtyProtPerGram = f.QtyProtPerGram,
            QtyCarbPerGram = f.QtyCarbPerGram,
            QtyFatPerGram = f.QtyFatPerGram,
            QtyCalPerGram = f.QtyCalPerGram,
        });

        return Results.Ok(foodsResponse);
    }
}
