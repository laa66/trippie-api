using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripGenerationController(ITripGenerationService tripGenerationService) : ControllerBase 
{
    private readonly ITripGenerationService _tripGenerationService = tripGenerationService;

    [HttpGet]
    public async Task<IActionResult> GetTrip() 
    {
        IEnumerable<TripPoint> points = await _tripGenerationService.CreateTrip(10, 17.07674062911932, 51.109818062431835, ["mountain_hut"]);
        return Ok(points);
    }
}