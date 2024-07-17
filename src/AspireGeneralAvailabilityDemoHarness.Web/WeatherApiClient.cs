namespace AspireGeneralAvailabilityDemoHarness.Web;

public class WeatherApiClient(HttpClient httpClient)
{
    public async Task<WeatherForecast[]> GetWeatherAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
    /*List<WeatherForecast>? forecasts = null;

    await foreach (var forecast in httpClient.GetFromJsonAsAsyncEnumerable<WeatherForecast>("/weatherforecast", cancellationToken))
    {
        if (forecasts?.Count >= maxItems)
        {
            break;
        }
        if (forecast is not null)
        {
            forecasts ??= [];
            forecasts.Add(forecast);
        }
    }

    return forecasts?.ToArray() ?? [];*/
    var response = await httpClient.PostAsJsonAsync<WeatherRequest>("/weatherforecast?dimension=C-137", new WeatherRequest { City = "Wellington" });
    throw new Exception("Exception in weather client");
    return [];
  }
}

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class WeatherRequest
{
  public string City { get; set; }
}