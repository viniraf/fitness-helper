using FitnessHelper.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/basalmetabolicrate")]
    [ApiController]
    public class BasalMetabolicRateController : ControllerBase
    {
        [SwaggerOperation(Tags = ["Basal Metabolic Rate"])]
        [HttpGet("withoutexercise")]
        public IResult WithoutExercise([FromQuery] Sex sex, double weight, double height, int age)
        {
            double basalMetabolicRate = 0;

            CommonController commonController = new CommonController();

            if (sex == Sex.Male)
            {
                basalMetabolicRate = commonController.BasalMetabolicRate(weight, height, age, sex);
            }

            if (sex == Sex.Female)
            {
                basalMetabolicRate = commonController.BasalMetabolicRate(weight, height, age, sex);
            }

            // TODO: Improve readability and transform into function
            //Fixed value for zero times per exercise week
            basalMetabolicRate = basalMetabolicRate * 1.2;

            int roundedBasalMetabolicRate = (int)Math.Round(basalMetabolicRate, MidpointRounding.AwayFromZero);

            BasalMetabolicRateModel basalMetabolicRateModel = new BasalMetabolicRateModel
            {
                RoundedBasalMetabolicRate = roundedBasalMetabolicRate,
                Type = "Without exercise"
            };

            return Results.Ok(basalMetabolicRateModel);
        }

        [SwaggerOperation(Tags = ["Basal Metabolic Rate"])]
        [HttpGet("withexercise")]
        public IResult WithExercise([FromQuery] Sex sex, double weight, double height, int age, [FromQuery] ExerciseTimesPerWeek exerciseTimesPerWeek)
        {
            double basalMetabolicRate = 0;

            CommonController commonController = new CommonController();

            if (sex == Sex.Male)
            {
                basalMetabolicRate = commonController.BasalMetabolicRate(weight, height, age, sex);
            }

            if (sex == Sex.Female)
            {
                basalMetabolicRate = commonController.BasalMetabolicRate(weight, height, age, sex);
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

            int roundedBasalMetabolicRate = (int)Math.Round(basalMetabolicRate, MidpointRounding.AwayFromZero);

            BasalMetabolicRateModel basalMetabolicRateModel = new BasalMetabolicRateModel
            {
                RoundedBasalMetabolicRate = roundedBasalMetabolicRate,
                Type = "With exercise"
            };

            return Results.Ok(basalMetabolicRateModel);

        }

    }
}
