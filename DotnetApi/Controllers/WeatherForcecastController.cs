
using Microsoft.AspNetCore.Mvc;
//alt+shift+f to format the code
using System.Runtime.InteropServices;

namespace DotnetApi.Controllers;

[ApiController] // send and recieve json data
[Route("[controller]")] // route to the controller
public class WeatherForecastController : ControllerBase
{

    public readonly string[] _summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
   
   [HttpGet("",Name= "GetWeatherForecast")]
   public IEnumerable<WeatherForecast> GetFiveDaysForecast()
    {
        
        var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            _summaries[Random.Shared.Next(_summaries.Length)]
        ))
        .ToArray();
    return forecast;
    }
}
public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

