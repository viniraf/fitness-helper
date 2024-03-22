﻿using FitnessHelper.Common;
using FitnessHelper.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitnessHelper.Endpoints.Bmr.WithoutExercise;

public class WithoutExercise
{

    public static string Template => "/bmr/withoutexercise";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action([FromQuery] Sex sex, double weight, double height, int age)
    {
        double basalMetabolicRate = 0;

        Calculations calculations = new Calculations();

        if (sex == Sex.Male)
        {
            basalMetabolicRate = calculations.MaleBasalMetabolicRate(weight, height, age);
        }
        else if (sex == Sex.Female)
        {
            basalMetabolicRate = calculations.FemaleBasalMetabolicRate(weight, height, age);
        }

        // TODO: Improve readability and transform into function
        //Fixed value for zero times per exercise week
        basalMetabolicRate = basalMetabolicRate * 1.2;

        int roundedBaseMetabolicRate = (int)Math.Round(basalMetabolicRate, MidpointRounding.AwayFromZero);

        return Results.Ok(new { BasalMetabolicRateWithoutExercise = $"{roundedBaseMetabolicRate} calories" });
    }

}
