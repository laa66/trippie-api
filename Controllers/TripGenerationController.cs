using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripGenerationController(IPoiApiService poiApiService) : ControllerBase 
{
    private readonly IPoiApiService _poiApiService = poiApiService;

    [HttpGet]
    public async Task<IActionResult> GetTrip() 
    {
       return Ok(await _poiApiService.GetPoiCollection("mountain_hut"));
    }
}