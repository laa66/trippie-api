using Controllers;
using Dtos;
using Moq;
using Services;
using Xunit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using TrippieApi.UnitTests.Fixtures;
using System.Security.Claims;
using Exceptions;
using Entities;

namespace TrippieApi.UnitTests.Systems.Controllers;

public class UnitTestUserController
{
    [Fact]
    public async void CreateUser_Should_Response200()
    {
        var userDTO = new UserDTO("email", "password", "password", "login");
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(s => s.Register(userDTO))
            .ReturnsAsync(IdentityResult.Success);
        
        var sut = new UserController(mockUserService.Object);
        var result = await sut.CreateUser(userDTO);
        
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void CreateUser_Should_Response400()
    {
        var userDTO = new UserDTO("email", "password", "password", "login");
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(s => s.Register(userDTO))
            .ReturnsAsync(IdentityResult.Failed());
        
        var sut = new UserController(mockUserService.Object);
        var result = await sut.CreateUser(userDTO);
        
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void GetUsers_Should_Response200()
    {
        var users = UserFixtures.GetUsers();
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(s => s.GetUsers())
            .Returns(users);
        
        var sut = new UserController(mockUserService.Object);
        var result = sut.GetUsers();
        var okObjectResult = result as OkObjectResult;
        Assert.Equal(users, okObjectResult?.Value);
    }

    [Fact]
    public void GetCurrentUser_Should_Response200()
    {
        var user = UserFixtures.GetUser();
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(s => s.GetUser(It.Is<string>(s => s.Equals("user-id"))))
            .Returns(user);
        
        var contextUser = new ClaimsPrincipal(new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, "user-id")]));

        var sut = new UserController(mockUserService.Object)
         {
            ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = contextUser
                }
            }
        };

        var result = sut.GetCurrentUser();
        var okObjectResult = result as OkObjectResult;
        Assert.Equal(user, okObjectResult?.Value);

    }

    [Fact]
    public void GetCurrentUser_Should_ThrowUserNotFoundException()
    {
        var mockUserService = new Mock<IUserService>();
        
        var contextUser = new ClaimsPrincipal(new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, "user-id")]));

        var sut = new UserController(mockUserService.Object)
         {
            ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        Assert.Throws<UserNotFoundException>(() => sut.GetCurrentUser());
    }

    [Fact]
    public void GetUser_Should_Response200()
    {
        var user = UserFixtures.GetUser();
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(s => s.GetUser(It.Is<string>(p => p.Equals("user-id"))))
            .Returns(user);

        var sut = new UserController(mockUserService.Object);

        var result = sut.GetUser("user-id");
        var okObjectResult = result as OkObjectResult;

        Assert.Equal(user, okObjectResult?.Value);
    }

    [Fact]
    public void GetUser_Should_ThrowUserNotFoundException()
    {
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(s => s.GetUser(It.Is<string>(p => p.Equals("wrong-id"))))
            .Throws(() => new UserNotFoundException("User not found"));

        var sut = new UserController(mockUserService.Object);
        
        Assert.Throws<UserNotFoundException>(() => sut.GetUser("wrong-id"));
    }

    
}