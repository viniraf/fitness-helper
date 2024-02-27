using FitnessHelper.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FitnessHelper.Endpoints.MacroCalculation.LoseWeight;

public class LoseWeight
{
    public static string Template => "/macrocalculation/loseweight";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(int basalMetabolicRate, double weight)
    {
        double loseWeightBasalMetabolicRate = basalMetabolicRate - 500;

        double totalCalories = loseWeightBasalMetabolicRate;

        // Calculate grams of protein
        double gramsOfProtein = weight * 1.8;

        // Subtract protein calories from total
        totalCalories = totalCalories - (gramsOfProtein * 4);

        // Calculate grams of fat
        double gramsOfFat = weight;

        // Subtract fat calories from total
        totalCalories = totalCalories - (gramsOfFat * 8);

        // Calculate grams of carb
        double gramsOfCarb = totalCalories / 4;

        return Results.Ok(
            new
            {
                BMR = $"{basalMetabolicRate} calories",
                LoseWeightBMR = $"{loseWeightBasalMetabolicRate} calories",
                ProtGram = $"{gramsOfProtein} g",
                ProtCal = $"{gramsOfProtein * 4} calories from protein",
                CarbGram = $"{gramsOfCarb} g",
                CarbCal = $"{gramsOfCarb * 4} calories from carb",
                FatGram = $"{gramsOfFat} g",
                FatCal = $"{gramsOfFat * 8} calories from fat",
            });
    }

}
