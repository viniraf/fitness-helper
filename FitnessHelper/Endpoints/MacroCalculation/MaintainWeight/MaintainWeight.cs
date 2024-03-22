using FitnessHelper.Common;
using FitnessHelper.Domain;
using FitnessHelper.Enums;

namespace FitnessHelper.Endpoints.MacroCalculation.MaintainWeight;

public class MaintainWeight
{
    public static string Template => "/macrocalculation/maintainweight";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(int basalMetabolicRate, double weight)
    {
        Calculations calculations = new Calculations();

        Goal goal = Goal.MaintainWeight;

        MacroCalculationClass macroCalculation = calculations.MacroCalculation(basalMetabolicRate, weight, goal);

        return Results.Ok(macroCalculation);
    }
}
