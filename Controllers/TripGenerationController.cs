using Dtos;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Controllers;

[ApiController]
[Route("api/trip/generation")]
public class TripGenerationController(ITripGenerationService tripGenerationService, TrippieContext trippieContext) : ControllerBase 
{
    private readonly ITripGenerationService _tripGenerationService = tripGenerationService;
    private readonly TrippieContext _trippieContext = trippieContext;

    /*
        implement IdentityController -> identity register, login, logout, reset password
        implement TripService -> create, delete, findAll, find, findAllUserTrips
    */
    [HttpPost]
    public async Task<IActionResult> GetTrip([FromQuery] int size, [FromQuery] double longitude, [FromQuery] double latitude, [FromBody] PoisDTO pois) 
    {
        IEnumerable<Models.TripPoint> points = await _tripGenerationService.CreateTrip(size, longitude, latitude, pois.Pois);
        return Ok(points);
    }
}