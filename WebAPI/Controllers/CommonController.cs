using FitnessHelper.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers;

public class CommonController : ControllerBase
{
    public CommonController()
    {
        
    }

    public double BasalMetabolicRate(double weight, double height, int age, Sex sex)
    {
        double bmr = 0;

        if (sex == Sex.Male)
        {
            bmr = (10 * weight) + (6.25 * height) - (5 * age) + 5;
        }

        if (sex == Sex.Female)
        {
            bmr = (10 * weight) + (6.25 * height) - (5 * age) + 161;
        }

        return bmr;
    }

    public MacronutrientCalculationModel MacronutrientCalculation(int basalMetabolicRate, double weight, Goal goal)
    {
        double goalBMR = 0;

        // TODO: Remove this logic from this location and place it in the correct location
        string goalString = "";

        if (goal == Goal.GainWeight)
        {
            goalBMR = basalMetabolicRate + 500;
            goalString = "Gain Weight";
        }

        if (goal == Goal.LoseWeight)
        {
            goalBMR = basalMetabolicRate - 500;
            goalString = "Lose Weight";
        }

        if (goal == Goal.MaintainWeight)
        {
            goalBMR = basalMetabolicRate;
            goalString = "Maintain Weight";
        }

        double totalCal = goalBMR;

        double protGram = weight * 1.8;

        totalCal = totalCal - (protGram * 4);

        double fatGram = weight;

        totalCal = totalCal - (fatGram * 8);

        double carbGram = totalCal / 4;

        MacronutrientCalculationModel macronutrientCalculationModel = new MacronutrientCalculationModel(basalMetabolicRate, goalString, goalBMR, protGram, carbGram, fatGram);

        return macronutrientCalculationModel;
    }

}
