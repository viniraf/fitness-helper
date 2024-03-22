using FitnessHelper.Common;
using FitnessHelper.Domain;
using FitnessHelper.Enums;

namespace FitnessHelper.Endpoints.MacroCalculation.GainWeight;

public class GainWeight
{
    public static string Template => "/macrocalculation/gainweight";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(int basalMetabolicRate, double weight)
    {
        Calculations calculations = new Calculations();

        Goal goal = Goal.GainWeight;

        MacroCalculationClass macroCalculation = calculations.MacroCalculation(basalMetabolicRate, weight, goal);

        return Results.Ok(macroCalculation);
    }
}
