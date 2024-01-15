using Entities;
using Microsoft.AspNetCore.Identity;
using Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class UserService([FromServices] IServiceProvider serviceProvider, TrippieContext trippieContext) : IUserService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly TrippieContext _trippieContext = trippieContext;

    public User GetUser(string id)
    {
        var user  = _trippieContext.Users.Find(id);
        return user ?? throw new Exception("User cannot be found");
    }

    public async Task<IdentityResult> Register(UserDTO userDTO)
    {
        var userManager = _serviceProvider.GetRequiredService<UserManager<User>>();

        User user = new()
        {
            JoinDate = DateTime.Now,
            Email = userDTO.Email,
            UserName = userDTO.Email,
            UserLogin = userDTO.UserLogin
        };

        return await userManager.CreateAsync(user, userDTO.Password);
    }

    public IEnumerable<User> GetUsers()
    {
        return [.. _trippieContext.Users];
    }
}