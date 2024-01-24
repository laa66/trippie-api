using Controllers;
using Entities;
using Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services;
using TrippieApi.UnitTests.Fixtures;
using Xunit;

namespace TrippieApi.UnitTests.Systems;

public class UnitTestTripController
{
    [Fact]
    public void GetTrip_Should_ResponseStatus200()
    {
        var trip = new Trip
        {
            TripId = 1
        };
        var mockTripService = new Mock<ITripService>();
        mockTripService.Setup(s => s.Find(1))
            .Returns(trip);

        var sut = new TripController(mockTripService.Object);
        var result = sut.GetTrip(1);

        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(trip, okObjectResult.Value);
    }

    [Fact]
    public void GetTrip_Should_ThrowTripNotFoundException()
    {
        var mockTripService = new Mock<ITripService>();
        mockTripService.Setup(s => s.Find(1))
            .Throws(new TripNotFoundException("Trip not found"));

        var sut = new TripController(mockTripService.Object);
        Assert.Throws<TripNotFoundException>(() => sut.GetTrip(1));
    }

    [Fact]
    public void DeleteTrip_Should_ResponsStatus204()
    {
        var mockTripService = new Mock<ITripService>();
        mockTripService.Setup(s => s.Delete(1))
            .Returns(true);

        var sut = new TripController(mockTripService.Object);
        var result = sut.DeleteTrip(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void DeleteTrip_Should_ResponsStatus404()
    {
        var mockTripService = new Mock<ITripService>();
        mockTripService.Setup(s => s.Delete(1))
            .Returns(false);

        var sut = new TripController(mockTripService.Object);
        var result = sut.DeleteTrip(1);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void GetAllTrips_Should_ResponseStatus200()
    {
        var trips = TripFixtures.GetAllTrips();
        var mockTripService = new Mock<ITripService>();
        mockTripService.Setup(s => s.FindAll())
            .Returns(trips);

        var sut = new TripController(mockTripService.Object);
        var result = sut.GetAllTrips();

        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(trips, okObjectResult.Value);
    }

    [Fact]
    public void GetAllUserTrips_Should_ResponseStatus200()
    {
        var trips = TripFixtures.GetAllTrips();
        var mockTripService = new Mock<ITripService>();
        mockTripService.Setup(s => s.FindUserTrips("id"))
            .Returns(trips);

        var sut = new TripController(mockTripService.Object);
        var result = sut.GetAllUserTrips("id");

        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(trips, okObjectResult.Value);
    }
}
