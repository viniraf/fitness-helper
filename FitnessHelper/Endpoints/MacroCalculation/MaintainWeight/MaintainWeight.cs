namespace FitnessHelper.Endpoints.MacroCalculation.MaintainWeight;

public class MaintainWeight
{
    public static string Template => "/macrocalculation/maintainweight";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(int basalMetabolicRate, double weight)
    {
        double maintainWeightBasalMetabolicRate = basalMetabolicRate;

        double totalCalories = maintainWeightBasalMetabolicRate;

        double gramsOfProtein = weight * 1.8;

        totalCalories = totalCalories - (gramsOfProtein * 4);

        double gramsOfFat = weight;

        totalCalories = totalCalories - (gramsOfFat * 8);

        double gramsOfCarb = totalCalories / 4;

        // TODO: Create response object
        return Results.Ok(
            new
            {
                BMR = $"{basalMetabolicRate} calories",
                MaintainWeightBMR = $"{maintainWeightBasalMetabolicRate} calories",
                ProtGram = $"{gramsOfProtein} g",
                ProtCal = $"{gramsOfProtein * 4} calories from protein",
                CarbGram = $"{gramsOfCarb} g",
                CarbCal = $"{gramsOfCarb * 4} calories from carb",
                FatGram = $"{gramsOfFat} g",
                FatCal = $"{gramsOfFat * 8} calories from fat",
            });
    }
}
