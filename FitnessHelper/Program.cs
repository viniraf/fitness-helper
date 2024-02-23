using FitnessHelper.Enums;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Runtime.ConstrainedExecution;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.UseAllOfToExtendReferenceSchemas();
    x.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/bmr/withoutexercise", ([FromQuery] Sex sex, double weight, double height, int age) =>
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

    //Fixed value for zero times per exercise week
    basalMetabolicRate = basalMetabolicRate * 1.2;

    int roundedBaseMetabolicRate = (int)Math.Round(basalMetabolicRate, MidpointRounding.AwayFromZero);

    return Results.Ok(new { BasalMetabolicRateWithoutExercise = $"{roundedBaseMetabolicRate} calories" });

})
.WithTags("Basal Metabolic Rate")
.WithMetadata(new SwaggerOperationAttribute("Returns Basal Metabolic Rate without considering exercise"));

app.MapGet("/bmr/withexercise", ([FromQuery] Sex sex, double weight, double height, int age, [FromQuery] ExerciseTimesPerWeek exerciseTimesPerWeek) =>
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
})
.WithTags("Basal Metabolic Rate")
.WithMetadata(new SwaggerOperationAttribute("Returns Basal Metabolic Rate considering exercise"));

app.MapGet("/macrodistribution/loseweight", (int basalMetabolicRate, double weight) =>
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

})
.WithTags("Macronutrient Distribution")
.WithMetadata(new SwaggerOperationAttribute("Returns macronutrient distribution for lose weight"));

app.MapGet("/macrodistribution/gainweight", (int basalMetabolicRate, double weight) =>
{
    double gainWeightBasalMetabolicRate = basalMetabolicRate + 500;

    double totalCalories = gainWeightBasalMetabolicRate;

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
            GainWeightBMR = $"{gainWeightBasalMetabolicRate} calories",
            ProtGram = $"{gramsOfProtein} g",
            ProtCal = $"{gramsOfProtein * 4} calories from protein",
            CarbGram = $"{gramsOfCarb} g",
            CarbCal = $"{gramsOfCarb * 4} calories from carb",
            FatGram = $"{gramsOfFat} g",
            FatCal = $"{gramsOfFat * 8} calories from fat",
        });
})
.WithTags("Macronutrient Distribution")
.WithMetadata(new SwaggerOperationAttribute("Returns macronutrient distribution for gain weight"));

app.Run();