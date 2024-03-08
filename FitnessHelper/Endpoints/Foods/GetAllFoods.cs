using FitnessHelper.Data;

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

        return Results.Ok(foods);
    }
}
