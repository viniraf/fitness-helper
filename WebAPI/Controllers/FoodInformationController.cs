using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers;

[Route("api/foodinformation")]
[ApiController]
public class FoodInformationController : ControllerBase
{

    [SwaggerOperation(Tags = ["Food Information"])]
    [HttpGet]
    public IResult GetAll(AppDbContext context)
    {
        List<FoodInformationModel>? foods = context.Foods.ToList();

        if (foods is null || !foods.Any())
        {
            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = $"No food information registered"
            };

            return Results.Problem(problemDetails);
        }

        IEnumerable<FoodInformationResponseModel> foodsResponse = foods.Select(f =>
        new FoodInformationResponseModel
        {
            Id = f.Id,
            Name = f.Name,
            UnitOfMeasurement = f.UnitOfMeasurement,
            Qty = f.Qty,
            QtyProt = f.QtyProt,
            QtyCarb = f.QtyCarb,
            QtyFat = f.QtyFat,
            QtyCal = f.QtyCal,
        });

        return Results.Ok(foodsResponse);
    }

    [SwaggerOperation(Tags = ["Food Information"])]
    [HttpGet("{id:int}")]
    public IResult GetById(AppDbContext context, int id)
    {
        IQueryable<FoodInformationModel>? foods = context.Foods.Where(f => f.Id == id);

        if (foods is null || !foods.Any())
        {
            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = $"No food information found with the id: {id}"
            };

            return Results.Problem(problemDetails);
        }

        IQueryable<FoodInformationResponseModel> foodsResponse = foods.Select(f =>
        new FoodInformationResponseModel
        {
            Id = f.Id,
            Name = f.Name,
            UnitOfMeasurement = f.UnitOfMeasurement,
            Qty = f.Qty,
            QtyProt = f.QtyProt,
            QtyCarb = f.QtyCarb,
            QtyFat = f.QtyFat,
            QtyCal = f.QtyCal,
        });

        return Results.Ok(foodsResponse);
    }

    [SwaggerOperation(Tags = ["Food Information"])]
    [HttpGet("{name}")]
    public IResult GetByName(AppDbContext context, string name)
    {
        string lowerName = name.ToLower();

        List<FoodInformationModel>? foods = context.Foods
            .Where(f => f.Name.ToLower().Contains(name))
            .ToList();

        if (foods is null || !foods.Any())
        {

            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = $"No food information found with the name: {name}"
            };

            return Results.Problem(problemDetails);
        }

        IEnumerable<FoodInformationResponseModel> foodsResponse = foods.Select(f =>
        new FoodInformationResponseModel
        {
            Id = f.Id,
            Name = f.Name,
            UnitOfMeasurement = f.UnitOfMeasurement,
            Qty = f.Qty,
            QtyProt = f.QtyProt,
            QtyCarb = f.QtyCarb,
            QtyFat = f.QtyFat,
            QtyCal = f.QtyCal,
        });

        return Results.Ok(foodsResponse);
    }

    [SwaggerOperation(Tags = ["Food Information"])]
    [HttpPost]
    public async Task<IResult> Post(FoodInformationRequestModel foodInformationRequest, AppDbContext context)
    {
        if (foodInformationRequest is null)
        {
            return Results.BadRequest("Please check the data and try again");
        }

        FoodInformationModel newFood = new FoodInformationModel();

        double qtyCalPerGram = (foodInformationRequest.QtyProt * 4) + (foodInformationRequest.QtyCarb * 4) + (foodInformationRequest.QtyFat * 7);

        newFood.Name = foodInformationRequest.Name;
        newFood.UnitOfMeasurement = foodInformationRequest.UnitOfMeasurement;
        newFood.Qty = foodInformationRequest.Qty;
        newFood.QtyProt = foodInformationRequest.QtyProt;
        newFood.QtyCarb = foodInformationRequest.QtyCarb;
        newFood.QtyFat = foodInformationRequest.QtyFat;
        newFood.QtyCal = qtyCalPerGram;

        await context.Foods.AddAsync(newFood);
        await context.SaveChangesAsync();

        return Results.Created($"/foodinformation/{newFood.Id}", $"{newFood.Name} created successfully");
    }

    [SwaggerOperation(Tags = ["Food Information"])]
    [HttpPut("{id:int}")]
    public async Task<IResult> Put([FromRoute] int id, FoodInformationRequestModel foodInformationRequest, AppDbContext context)
    {
        FoodInformationModel? food = context.Foods.FirstOrDefault(f => f.Id == id);

        if (food is null)
        {
            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = $"No food information found with the id: {id}"
            };

            return Results.Problem(problemDetails);
        }

        double qtyCalPerGram = (foodInformationRequest.QtyProt * 4) + (foodInformationRequest.QtyCarb * 4) + (foodInformationRequest.QtyFat * 7);

        food.Name = foodInformationRequest.Name;
        food.UnitOfMeasurement = foodInformationRequest.UnitOfMeasurement;
        food.Qty = foodInformationRequest.Qty;
        food.QtyProt = foodInformationRequest.QtyProt;
        food.QtyCarb = foodInformationRequest.QtyCarb;
        food.QtyFat = foodInformationRequest.QtyFat;
        food.QtyCal = qtyCalPerGram;

        await context.SaveChangesAsync();

        return Results.Ok("Food information has been update!");
    }

    [SwaggerOperation(Tags = ["Food Information"])]
    [HttpDelete("{id:int}")]
    public async Task<IResult> Delete([FromRoute] int id, AppDbContext context)
    {
        FoodInformationModel? food = context.Foods.FirstOrDefault(f => f.Id == id);

        if (food is null)
        {
            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = $"No food information found with the id: {id}"
            };

            return Results.Problem(problemDetails);
        }

        context.Remove(food);
        await context.SaveChangesAsync();

        return Results.NoContent();
    }

}