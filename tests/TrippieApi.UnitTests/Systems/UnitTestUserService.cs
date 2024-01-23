using Entities;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Services;
using Xunit;

namespace TrippieApi.UnitTests.Systems;

public class UnitTestUserService
{


    [Fact]
    public void GetUser_ShouldGetUserById()
    {
        var testUser = new User
        {
            Id = "user"
        };

        var mockServiceProvider = new Mock<IServiceProvider>();
        var options = new DbContextOptionsBuilder<TrippieContext>()
            .UseInMemoryDatabase("TestMemoryDb")
            .Options;
        var context = new TrippieContext(options);

        context.Users.Add(testUser);
        context.SaveChanges();

        var sut = new UserService(mockServiceProvider.Object, context);

        var result = sut.GetUser("user");

        Assert.Equal(testUser, result);

        context.Database.EnsureDeleted();
    }

    [Fact]
    public void GetUser_ShouldThrowUserNotFoundException()
    {
        var mockServiceProvider = new Mock<IServiceProvider>();
        var options = new DbContextOptionsBuilder<TrippieContext>()
            .UseInMemoryDatabase("TestMemoryDb")
            .Options;
        var context = new TrippieContext(options);

        var sut = new UserService(mockServiceProvider.Object, context);

        Assert.Throws<UserNotFoundException>(() => sut.GetUser("user"));
    }

    [Fact]
    public void GetUser_ShouldGetUsers()
    {
        var testUser1 = new User
        {
            Id = "user1"
        };

        var testUser2 = new User
        {
            Id = "user2"
        };

        var mockServiceProvider = new Mock<IServiceProvider>();
        var options = new DbContextOptionsBuilder<TrippieContext>()
            .UseInMemoryDatabase("TestMemoryDb")
            .Options;
        var context = new TrippieContext(options);

        context.Users.Add(testUser1);
        context.Users.Add(testUser2);
        context.SaveChanges();

        

        var sut = new UserService(mockServiceProvider.Object, context);

        var result = sut.GetUsers().ToArray();

        Assert.Equal(2, result.Length);
        Assert.Equal("user1", result[0].Id);
        Assert.Equal("user2", result[1].Id);

        context.Database.EnsureDeleted();
    }
}