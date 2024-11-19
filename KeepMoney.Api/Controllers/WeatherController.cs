using KeepMoney.Api.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeepMoney.Api.Controllers;


public class WeatherController : ApiController
{
    private readonly string[] _summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    [HttpGet]
    [AllowAnonymous]
    public ActionResult<IEnumerable<WeatherForecast>> Get()
    {
        var forecast =  Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                _summaries[Random.Shared.Next(_summaries.Length)]
            ))
            .ToArray();
        return Ok(forecast);
    }

    [HttpGet("WithToken")]
    [Authorize]
    public ActionResult<IEnumerable<WeatherForecast>> GetWithToken()
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                _summaries[Random.Shared.Next(_summaries.Length)]
            ))
            .ToArray();
        return Ok(forecast);
    }
}