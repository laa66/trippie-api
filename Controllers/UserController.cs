using Dtos;
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
}