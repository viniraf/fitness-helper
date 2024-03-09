using FitnessHelper.Data;

namespace FitnessHelper.Endpoints.Foods;

public class GetFoodsByName
{
    public static string Template => "/foods/{name}";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(AppDbContext context, string name)
    {

        string lowerName = name.ToLower();

        var foods = context.Foods
            .Where(f => f.Name.ToLower().Contains(name))
            .ToList();

        if (foods is null || !foods.Any())
        {
            return Results.NotFound($"No food found with the name: {name}");
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
