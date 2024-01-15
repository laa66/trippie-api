using Dtos;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Controllers;

[ApiController]
[Route("api/trip/generation")]
public class TripGenerationController(ITripGenerationService tripGenerationService,
    ITripService tripService,
    IUserService userService) : ControllerBase 
{
    private readonly ITripGenerationService _tripGenerationService = tripGenerationService;
    private readonly ITripService _tripService = tripService;
    private readonly IUserService _userService = userService;
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> GetTrip([FromQuery] int size, [FromQuery] double longitude, [FromQuery] double latitude, [FromBody] PoisDTO pois) 
    {
        IEnumerable<Models.TripPoint> points = await _tripGenerationService.CreateTrip(size, longitude, latitude, pois.Pois);
        string? id = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("User is not available");
        
        var user = _userService.GetUser(id);

        _tripService.Save(user, new Trip {
            TripId = 0,
            User = user,
            Id = user.Id,
            Title = "default-trip",
            TripDate = DateTime.Now,
            TripPoints = points.Select(p => new Entities.TripPoint
            {
                TripPointId = 0,
                Longitude = p.Coordinates[0],
                Latitude = p.Coordinates[1],
                Name = p.Name,
                FullAddress = p.FullAddress
            }).ToList()
        });
        
        return Ok(points);
    }
}