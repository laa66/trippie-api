using Entities;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Services;
using TrippieApi.UnitTests.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace TrippieApi.UnitTests.Systems;

public class UnitTestTripService(ITestOutputHelper outputHelper)
{
    private readonly ITestOutputHelper _outputHelper = outputHelper;

    public TrippieContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<TrippieContext>()
                    .UseInMemoryDatabase("random")
                    .Options;
        return new TrippieContext(options);
    }

    [Fact]
    public void Delete_Should_DeleteUser()
    {
        var context = CreateContext();
        var sut = new TripService(context);

        context.Trips.Add(TripFixtures.GetTrip());
        context.SaveChanges();

        var findResult = sut.Find(1);
        Assert.Equal(1, findResult.TripId);

        var deleteResult = sut.Delete(1);
        Assert.True(deleteResult);
        Assert.Throws<TripNotFoundException>(() => sut.Find(1));
    }

    [Fact]
    public void Delete_Should_NotDeleteUser()
    {
        var context = CreateContext();
        var sut = new TripService(context);

        Assert.Throws<TripNotFoundException>(() => sut.Delete(1));
    }

    [Fact]
    public void Find_Should_FindUser()
    {
        var context = CreateContext();
        context.Trips.Add(TripFixtures.GetTrip());
        context.SaveChanges();

        var sut = new TripService(context);

        var result = sut.Find(1);

        Assert.Equal(1, result.TripId);
        context.Database.EnsureDeleted();
    }

    [Fact]
    public void Find_Should_NotFindUser()
    {
        var context = CreateContext();
        var sut = new TripService(context);

        Assert.Throws<TripNotFoundException>(() => sut.Find(2));
    }

    [Fact]
    public void FindAll_Should_FindAll()
    {
        var context = CreateContext();
        TripFixtures.GetAllTrips().ForEach(a => context.Trips.Add(a));
        context.SaveChanges();

        var sut = new TripService(context);

        var result = sut.FindAll().ToList();

        Assert.Equal(3, result.Count);
        Assert.Equal(1, result[0].TripId);
        Assert.Equal(2, result[1].TripId);
        Assert.Equal(3, result[2].TripId);

        context.Database.EnsureDeleted();
    }

    [Fact]
    public void FindUserTrips_Should_FindUserTrips()
    {
        var context = CreateContext();
        TripFixtures.GetAllTrips().ForEach(a => context.Trips.Add(a));
        context.SaveChanges();

        var sut = new TripService(context);

        var result = sut.FindUserTrips("userId1");

        Assert.Equal(2, result.Count());

        context.Database.EnsureDeleted();
        
    }

    [Fact]
    public void FindUserTrips_Should_NotFindUserTrips()
    {
        var context = CreateContext();
        TripFixtures.GetAllTrips().ForEach(a => context.Trips.Add(a));
        context.SaveChanges();

        var sut = new TripService(context);

        var result = sut.FindUserTrips("userId3");

        Assert.Empty(result);

        context.Database.EnsureDeleted();
    }

    [Fact]
    public void Save_Should_SaveUserTrip()
    {
        var user = new User
        {
            Id = "userId1"
        };
        var context = CreateContext();
        context.Users.Add(user);
        context.SaveChanges();
        var sut = new TripService(context);

        var trip = TripFixtures.GetTrip();
        trip.TripId = 0;
        var result = sut.Save(user, trip);

        context.SaveChanges();

        Assert.Equal(1, result.UserTrips.First().TripId);

        context.Database.EnsureDeleted();
    }
}