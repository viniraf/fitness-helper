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

app.Run();