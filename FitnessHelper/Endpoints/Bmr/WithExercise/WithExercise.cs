using FitnessHelper.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FitnessHelper.Endpoints.Bmr.WithExercise;

public class WithExercise
{
    public static string Template => "/bmr/withexercise";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action([FromQuery] Sex sex, double weight, double height, int age, [FromQuery] ExerciseTimesPerWeek exerciseTimesPerWeek)
    {
        double basalMetabolicRate = 0;

        if (sex == Sex.Male)
        {
            basalMetabolicRate = (10 * weight) + (6.25 * height) - (5 * age) + 5;
        }
        else if (sex == Sex.Female)
        {
            basalMetabolicRate = (10 * weight) + (6.25 * height) - (5 * age) + 161;
        }

        Dictionary<int, double> multiplicativeFactorMap = new Dictionary<int, double>
        {
            {0, 1.2},
            {1, 1.375},
            {2, 1.375},
            {3, 1.55},
            {4, 1.55},
            {5, 1.55},
            {6, 1.725},
            {7, 1.725}
        };

        double multiplicativeFactor = multiplicativeFactorMap[Convert.ToInt32(exerciseTimesPerWeek)];

        basalMetabolicRate = basalMetabolicRate * multiplicativeFactor;

        int roundedBaseMetabolicRate = (int)Math.Round(basalMetabolicRate, MidpointRounding.AwayFromZero);


        return Results.Ok(new { BasalMetabolicRateWithExercise = $"{roundedBaseMetabolicRate} calories" });
    }
}
