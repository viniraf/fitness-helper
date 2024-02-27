using FitnessHelper.Endpoints.Bmr.WithExercise;
using FitnessHelper.Endpoints.Bmr.WithoutExercise;
using FitnessHelper.Endpoints.Macrodistribution.GainWeight;
using FitnessHelper.Endpoints.Macrodistribution.LoseWeight;
using FitnessHelper.Endpoints.Macrodistribution.MaintainWeight;
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


app.MapMethods(WithoutExercise.Template, WithoutExercise.Methods, WithoutExercise.Handle)
.WithTags("Basal Metabolic Rate")
.WithMetadata(new SwaggerOperationAttribute("Returns Basal Metabolic Rate without considering exercise"));

app.MapMethods(WithExercise.Template, WithExercise.Methods, WithExercise.Handle)
.WithTags("Basal Metabolic Rate")
.WithMetadata(new SwaggerOperationAttribute("Returns Basal Metabolic Rate considering exercise"));

app.MapMethods(LoseWeight.Template, LoseWeight.Methods, LoseWeight.Handle)
.WithTags("Macronutrient Distribution")
.WithMetadata(new SwaggerOperationAttribute("Returns macronutrient distribution for lose weight"));

app.MapMethods(GainWeight.Template, GainWeight.Methods, LoseWeight.Handle)
.WithTags("Macronutrient Distribution")
.WithMetadata(new SwaggerOperationAttribute("Returns macronutrient distribution for gain weight"));

app.MapMethods(MaintainWeight.Template, MaintainWeight.Methods, MaintainWeight.Handle)
.WithTags("Macronutrient Distribution")
.WithMetadata(new SwaggerOperationAttribute("Returns macronutrient distribution for maintain weight"));

app.Run();