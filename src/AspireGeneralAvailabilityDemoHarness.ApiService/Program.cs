using AspireGeneralAvailabilityDemoHarness.ApiService;
using Microsoft.AspNetCore.Mvc;
using Raygun4Aspire;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

builder.AddRaygun();

var app = builder.Build();



// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.UseRaygun();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
  /*var forecast = Enumerable.Range(1, 5).Select(index =>
      new WeatherForecast
      (
          DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
          Random.Shared.Next(-20, 55),
          summaries[Random.Shared.Next(summaries.Length)]
      ))
      .ToArray();
  return forecast;*/
  throw new AccessViolationException("All your weather are belong to us");
});

app.MapPost("/weatherforecast", async (HttpContext context, RaygunClient raygunClient, [FromBody] WeatherRequest requestBody) =>
{
  try
  {
    throw new Exception("An inner exception");
  }
  catch (Exception ex)
  {
    try
    {
      var exception = new InvalidOperationException("It's never not windy in Wellington", ex);
      exception.Data["correlationId"] = "123ABC";
      exception.Data["model"] = new { height = 285, strength = "full", yes = false, peripherals = new List<string>() { "mouse", "monitor", "cassette reader" } };
      throw exception;
    }
    catch (Exception ex2)
    {
      await raygunClient.SendInBackground(ex, context: context);
    }
  }
});

app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
