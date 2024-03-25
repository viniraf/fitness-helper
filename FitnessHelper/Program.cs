using FitnessHelper.Data;
using FitnessHelper.Endpoints.Bmr.WithExercise;
using FitnessHelper.Endpoints.Bmr.WithoutExercise;
using FitnessHelper.Endpoints.Foods;
using FitnessHelper.Endpoints.MacroCalculation.GainWeight;
using FitnessHelper.Endpoints.MacroCalculation.LoseWeight;
using FitnessHelper.Endpoints.MacroCalculation.MaintainWeight;
using FitnessHelper.Endpoints.NutritionalInformation;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

// Caminho para o arquivo do banco de dados
var dbPath = Path.Combine("Data", "Database.db");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.UseAllOfToExtendReferenceSchemas();
    x.EnableAnnotations();
});
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite($"Data Source={dbPath}");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapMethods(WithoutExercise.Template, WithoutExercise.Methods, WithoutExercise.Handle)
.WithTags("1. Basal Metabolic Rate")
.WithMetadata(new SwaggerOperationAttribute("Returns the amount of calories burned by the body during the day without considering physical exercise"));

app.MapMethods(WithExercise.Template, WithExercise.Methods, WithExercise.Handle)
.WithTags("1. Basal Metabolic Rate")
.WithMetadata(new SwaggerOperationAttribute("Returns the amount of calories burned by the body during the day considering physical exercise"));

app.MapMethods(LoseWeight.Template, LoseWeight.Methods, LoseWeight.Handle)
.WithTags("2. Macronutrient Calculation")
.WithMetadata(new SwaggerOperationAttribute("Returns the necessary amount of each macronutrient per day to lose weight"));

app.MapMethods(GainWeight.Template, GainWeight.Methods, GainWeight.Handle)
.WithTags("2. Macronutrient Calculation")
.WithMetadata(new SwaggerOperationAttribute("Returns the necessary amount of each macronutrient per day to gain weight"));

app.MapMethods(MaintainWeight.Template, MaintainWeight.Methods, MaintainWeight.Handle)
.WithTags("2. Macronutrient Calculation")
.WithMetadata(new SwaggerOperationAttribute("Returns the necessary amount of each macronutrient per day to maintain weight"));

app.MapMethods(GetAllFoods.Template, GetAllFoods.Methods, GetAllFoods.Handle)
.WithTags("3. Foods and Nutritional Information")
.WithMetadata(new SwaggerOperationAttribute("Returns all foods registered with the nutritional information of each"));

app.MapMethods(PostFoods.Template, PostFoods.Methods, PostFoods.Handle)
.WithTags("3. Foods and Nutritional Information")
.WithMetadata(new SwaggerOperationAttribute("Insert a new food and the nutritional information"));

app.MapMethods(GetFoodsByName.Template, GetFoodsByName.Methods, GetFoodsByName.Handle)
.WithTags("3. Foods and Nutritional Information")
.WithMetadata(new SwaggerOperationAttribute("Returns all foods registered with the nutritional information of each containing the provided text in their names"));

app.MapMethods(GetFoodById.Template, GetFoodById.Methods, GetFoodById.Handle)
.WithTags("3. Foods and Nutritional Information")
.WithMetadata(new SwaggerOperationAttribute("Returns the nutricional information of the food corresponding to the provided Id"));

app.Run();