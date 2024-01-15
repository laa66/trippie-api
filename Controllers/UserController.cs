using System.Security.Claims;
using Dtos;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

[ApiController]
[Route("api/user")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
    {
        var result = await _userService.Register(userDTO);
        return result.Succeeded ? 
            Ok(new { Message = "User registered successfully" }) : 
            BadRequest(new { Message = "Registration failed", result.Errors });
    }

    [HttpGet("all")]
    [Authorize]
    public IActionResult GetUsers()
    {
        return Ok(_userService.GetUsers());
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult GetUser()
    {
        string? id = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("User is not available");
        return Ok(_userService.GetUser(id));
    }
}