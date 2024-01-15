using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

[ApiController]
[Route("api/trip")]
[Authorize]
public class TripController(ITripService tripService) : ControllerBase
{
    private readonly ITripService _tripService = tripService;

    [HttpGet]
    public IActionResult GetTrip([FromQuery] int id)
    {
        return Ok(_tripService.Find(id));
    }

    [HttpDelete]
    public IActionResult DeleteTrip([FromQuery] int id)
    {
        _tripService.Delete(id);
        return NoContent();
    }

    [HttpGet("all")]
    public IActionResult GetAllTrips()
    {
        return Ok(_tripService.FindAll());
    }

    [HttpGet("user/all")]
    public IActionResult GetAllUserTrips([FromQuery] string id)
    {
        return Ok(_tripService.FindUserTrips(id));
    }
}