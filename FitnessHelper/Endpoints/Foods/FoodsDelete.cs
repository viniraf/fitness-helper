using FitnessHelper.Data;
using FitnessHelper.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FitnessHelper.Endpoints.Foods;

public class FoodsDelete
{
    public static string Template => "/foods{id:int}";

    public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };

    public static Delegate Handle => Action;

    public static async Task<IResult> Action([FromRoute] int id, AppDbContext context)
    {
        FoodsClass? food = context.Foods.FirstOrDefault(f => f.Id == id);

        if (food is null)
        {
            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = $"No food found with the id: {id}"
            };

            return Results.Problem(problemDetails);
        }

        context.Remove(food);
        await context.SaveChangesAsync();
        
        return Results.NoContent();
    }
}
