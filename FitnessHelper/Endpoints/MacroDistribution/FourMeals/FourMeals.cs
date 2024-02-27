namespace FitnessHelper.Endpoints.MacroDistribution.FourMeals;

public class FourMeals
{
    public static string Template => "/macrodistribution/fourmeals";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action()
    {
        return Results.Ok("In development");
    }
}
