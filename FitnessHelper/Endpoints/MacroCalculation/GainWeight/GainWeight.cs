namespace FitnessHelper.Endpoints.MacroCalculation.GainWeight;

public class GainWeight
{
    public static string Template => "/macrocalculation/gainweight";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(int basalMetabolicRate, double weight)
    {
        double gainWeightBasalMetabolicRate = basalMetabolicRate + 500;

        double totalCalories = gainWeightBasalMetabolicRate;

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
                GainWeightBMR = $"{gainWeightBasalMetabolicRate} calories",
                ProtGram = $"{gramsOfProtein} g",
                ProtCal = $"{gramsOfProtein * 4} calories from protein",
                CarbGram = $"{gramsOfCarb} g",
                CarbCal = $"{gramsOfCarb * 4} calories from carb",
                FatGram = $"{gramsOfFat} g",
                FatCal = $"{gramsOfFat * 8} calories from fat",
            });
    }
}
