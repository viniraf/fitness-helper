using FitnessHelper.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Models;

namespace WebAPI.Controllers;

[Route("api/macronutrientcalculation")]
[ApiController]
public class MacronutrientCalculationController : ControllerBase
{

    [SwaggerOperation(Tags = ["Macronutrient Calculation"])]
    [HttpGet("gainweight")]
    public IResult GainWeight(int basalMetabolicRate, double weight)
    {
        CommonController commonController = new CommonController();

        MacronutrientCalculationModel macronutrientCalculationModel = new MacronutrientCalculationModel();

        Goal goal = Goal.GainWeight;

        macronutrientCalculationModel = commonController.MacronutrientCalculation(basalMetabolicRate, weight, goal);

        return Results.Ok(macronutrientCalculationModel);
    }

    [SwaggerOperation(Tags = ["Macronutrient Calculation"])]
    [HttpGet("loseweight")]
    public IResult LoseWeight(int basalMetabolicRate, double weight)
    {
        CommonController commonController = new CommonController();

        MacronutrientCalculationModel macronutrientCalculationModel = new MacronutrientCalculationModel();

        Goal goal = Goal.LoseWeight;

        macronutrientCalculationModel = commonController.MacronutrientCalculation(basalMetabolicRate, weight, goal);

        return Results.Ok(macronutrientCalculationModel);
    }

    [SwaggerOperation(Tags = ["Macronutrient Calculation"])]
    [HttpGet("maintainweight")]
    public IResult MaitainWeight(int basalMetabolicRate, double weight)
    {
        CommonController commonController = new CommonController();

        MacronutrientCalculationModel macronutrientCalculationModel = new MacronutrientCalculationModel();

        Goal goal = Goal.MaintainWeight;

        macronutrientCalculationModel = commonController.MacronutrientCalculation(basalMetabolicRate, weight, goal);

        return Results.Ok(macronutrientCalculationModel);
    }
}
