namespace FitnessHelper.Endpoints.MacroDistribution.SixMeals;

public class SixMeals
{
    public static string Template => "/macrodistribution/sixmeals";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action()
    {
        return Results.Ok("In development");
    }
}
