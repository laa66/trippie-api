using Dtos;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripGenerationController(ITripGenerationService tripGenerationService) : ControllerBase 
{
    private readonly ITripGenerationService _tripGenerationService = tripGenerationService;

    [HttpPost]
    public async Task<IActionResult> GetTrip([FromQuery] int size, [FromQuery] double longitude, [FromQuery] double latitude, [FromBody] PoisDTO pois) 
    {
        IEnumerable<TripPoint> points = await _tripGenerationService.CreateTrip(size, longitude, latitude, pois.Pois);
        return Ok(points);
    }
}