using FitnessHelper.Common;
using FitnessHelper.Domain;
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
        Calculations calculations = new Calculations();

        Goal goal = Goal.LoseWeight;

        MacroCalculationClass macroCalculationResult = calculations.MacroCalculation(basalMetabolicRate, weight, goal);

        return Results.Ok(macroCalculationResult);
    }

}
