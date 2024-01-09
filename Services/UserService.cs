using Entities;
using Microsoft.AspNetCore.Identity;
using Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Services;

public class UserService([FromServices] IServiceProvider serviceProvider) : IUserService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task<IdentityResult> Register(UserDTO userDTO)
    {
        var userManager = _serviceProvider.GetRequiredService<UserManager<User>>();

        User user = new()
        {
            JoinDate = DateTime.Now,
            Email = userDTO.Email,
            UserName = userDTO.Username
        };

        return await userManager.CreateAsync(user, userDTO.Password);
    }
}